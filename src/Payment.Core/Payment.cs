using System;
using Payment.Core.Utility;

namespace Payment.Core
{
    public class Payment : IPayment
    {
        public PaymentId PaymentId { get; }
        public string CardNumber { get; }
        public decimal Amount { get; }
        public string Currency { get; }
        public DateTimeOffset OperationDate { get; }
        public Result<BankAuthorizationId> BankAuthorizationResult { get; }

        public Payment(
            PaymentId paymentId,
            string cardNumber,
            decimal amount, 
            string currency, 
            DateTimeOffset operationDate, 
            Result<BankAuthorizationId> bankAuthorizationResult)
        {
            PaymentId = paymentId;
            CardNumber = cardNumber;
            Amount = amount;
            Currency = currency;
            OperationDate = operationDate;
            BankAuthorizationResult = bankAuthorizationResult;
        }
    }
}
