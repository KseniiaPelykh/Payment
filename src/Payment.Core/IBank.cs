using System;
using System.Threading.Tasks;

namespace Payment.Core
{
    public interface IBank
    {
        Task<BankResponse> Authorize(BankRequest request);
    }
}
