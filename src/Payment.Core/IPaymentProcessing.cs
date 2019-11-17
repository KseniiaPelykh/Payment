using System.Threading.Tasks;
using Payment.Core.Utility;

namespace Payment.Core
{
    public interface IPaymentProcessing
    {
        Task<Result<PaymentId>> ProcessAsync(ValidPaymentRequest request);
    }
}
