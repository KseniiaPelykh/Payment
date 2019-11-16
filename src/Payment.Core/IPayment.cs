using System;
using Payment.Core.Utility;

namespace Payment.Core
{
    public interface IPayment
    {
        PaymentId PaymentId { get; }
        string CardNumber { get; }
        decimal Amount { get; }
        string Currency { get; }
        DateTimeOffset OperationDate { get; }
        Result<BankAuthorizationId> BankAuthorizationResult { get; }
    }
}
