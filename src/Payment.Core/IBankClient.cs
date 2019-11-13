using System.Threading.Tasks;

namespace Payment.Core
{
    public interface IBankClient
    {
        Task<BankRequest> CreateBankRequest(PaymentData data);
        Task<Result<BankRequestId>> Authorize(BankRequest request);
    }
}
