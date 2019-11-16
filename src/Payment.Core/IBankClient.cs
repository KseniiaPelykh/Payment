using System.Threading.Tasks;
using Payment.Core.Utility;

namespace Payment.Core
{
    public interface IBankClient
    {
        Task<BankRequest> CreateBankRequestAsync(ValidPaymentData data);
        Task<Result<BankRequestId>> AuthorizeAsync(BankRequest request);
    }
}
