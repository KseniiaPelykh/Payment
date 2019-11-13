using System.Threading.Tasks;

namespace Payment.Core
{
    public class ProcessPayment
    {
        private readonly IBankClient _bankClient;

        public ProcessPayment(IBankClient bankClient)
        {
            _bankClient = bankClient;
        }

        public async Task<Result<PaymentId>> Process(PaymentData data)
        {
            var authorizationRequest = await _bankClient.CreateBankRequest(data);
            var authorizationResult = await _bankClient.Authorize(authorizationRequest);

            return authorizationResult.IsSuccess
                ? Result<PaymentId>.CreateSuccess(new PaymentId("TODO"))
                : Result<PaymentId>.CreateFailure();
        }
    }
}
