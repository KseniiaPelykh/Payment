using System;
using System.Threading.Tasks;
using Payment.Core;
using Payment.Core.Utility;

namespace Payment.Bank
{
    public class BankClient : IBankClient
    {
        public Task<BankRequest> CreateBankRequestAsync(ValidPaymentRequest data) =>
             Task.FromResult(new BankRequest(string.Empty));

        public Task<Result<BankAuthorizationId>> AuthorizeAsync(BankRequest request) =>
            Task.FromResult(
                Result<BankAuthorizationId>.CreateSuccess(
                    new BankAuthorizationId(Guid.NewGuid().ToString("N"))));
    }
}
