using System.Threading.Tasks;

namespace Payment.Core
{
    public interface IBankClient
    {
        Task<BankRequest> CreateBankRequestAsync(ValidPaymentData data);
        Task<Result<BankRequestId>> AuthorizeAsync(BankRequest request);
    }
}
