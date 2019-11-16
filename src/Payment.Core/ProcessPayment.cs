using System.Threading.Tasks;

namespace Payment.Core
{
    public class ProcessPayment
    {
        private readonly IBankClient _bankClient;
        private readonly IPaymentRepository _repository;

        public ProcessPayment(IBankClient bankClient, IPaymentRepository repository)
        {
            _bankClient = bankClient;
            _repository = repository;
        }

        public async Task<Result<PaymentId>> ProcessAsync(ValidPaymentData data)
        {
            var authorizationRequest = await _bankClient.CreateBankRequestAsync(data);
            var authorizationResult = await _bankClient.AuthorizeAsync(authorizationRequest);

            if (authorizationResult.IsSuccess)
            {
                var paymentId = PaymentId.GenerateNew();
                await _repository.PutAsync(data, paymentId);
                return Result<PaymentId>.CreateSuccess(paymentId);
            }

            return Result<PaymentId>.CreateFailure();
        }
    }
}
