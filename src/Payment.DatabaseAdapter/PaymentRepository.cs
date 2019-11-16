using System;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Util;
using Payment.Core;
using Payment.Core.Utility;

namespace Payment.DatabaseAdapter
{
    public class PaymentRepository : IPaymentRepository
    {
        public async Task PutAsync(IPayment payment)
        {
             using (var context = new DynamoDBContext(new AmazonDynamoDBClient()))
            {
                var dbPayment = new Payment
                {
                    PaymentId = payment.PaymentId.Value,
                    CardNumber = payment.CardNumber,
                    Amount = payment.Amount,
                    Currency = payment.Currency,
                    OperationDate = GetStringFromDateTimeValue(payment.OperationDate)
                };

                if (payment.BankAuthorizationResult.IsSuccess)
                {
                    dbPayment.Success = true;
                    var bankAuthorizationId = payment.BankAuthorizationResult.Value;
                    dbPayment.BankAuthorizationId = bankAuthorizationId.Value;
                }

                await context.SaveAsync(dbPayment);
            }
        }

        public async Task<IPayment> GetAsync(PaymentId id)
        {
            using (var context = new DynamoDBContext(new AmazonDynamoDBClient()))
            {
                var payment = await context.LoadAsync<Payment>(id.Value);

                var bankAuthorization = payment.Success
                        ? Result<BankAuthorizationId>.CreateSuccess(new BankAuthorizationId(payment.BankAuthorizationId))
                        : Result<BankAuthorizationId>.CreateFailure();

                return new Core.Payment(
                    paymentId: new PaymentId(payment.PaymentId),
                    cardNumber: payment.CardNumber,
                    amount: payment.Amount,
                    currency: payment.Currency,
                    operationDate: GetTimestampFromStringValue(payment.OperationDate),
                    bankAuthorizationResult: bankAuthorization);
            }
        }

        private static string GetStringFromDateTimeValue(DateTimeOffset timestamp) =>
            AWSSDKUtils.ConvertToUnixEpochSeconds(timestamp.DateTime).ToString();

        private static DateTimeOffset GetTimestampFromStringValue(string seconds) =>
            AWSSDKUtils.ConvertFromUnixEpochSeconds(int.Parse(seconds));
    }
}
