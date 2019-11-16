using System.Threading.Tasks;
using Payment.Core.Utility;

namespace Payment.Core
{
    public interface IBankClient
    {
        Task<BankRequest> CreateBankRequestAsync(ValidPaymentRequest data);
        Task<Result<BankAuthorizationId>> AuthorizeAsync(BankRequest request);
    }
}
