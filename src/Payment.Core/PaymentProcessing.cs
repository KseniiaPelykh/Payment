using System;
using System.Threading.Tasks;
using Payment.Core.Utility;

namespace Payment.Core
{
    public class PaymentProcessing : IPaymentProcessing
    {
        private readonly IBankClient _bankClient;
        private readonly IPaymentRepository _repository;

        public PaymentProcessing(IBankClient bankClient, IPaymentRepository repository)
        {
            _bankClient = bankClient;
            _repository = repository;
        }

        public async Task<Result<PaymentId>> ProcessAsync(ValidPaymentRequest request)
        {
            var authorizationRequest = await _bankClient.CreateBankRequestAsync(request);
            var authorizationResult = await _bankClient.AuthorizeAsync(authorizationRequest);

            var paymentId = PaymentId.GenerateNew();

            try
            {
                var payment = new Payment(
                    paymentId: paymentId,
                    cardNumber: Masking.GetMask(request.CardNumber),
                    amount: request.Amount,
                    currency: request.Currency,
                    operationDate: DateTimeOffset.UtcNow,
                    bankAuthorizationResult: authorizationResult);

                await _repository.PutAsync(payment);
            }
            catch (Exception e)
            {
                //write to log that it some problem with storing request
            }

            return authorizationResult.IsSuccess
                ? Result<PaymentId>.CreateSuccess(paymentId)
                : Result<PaymentId>.CreateFailure(paymentId);
        }
    }
}
