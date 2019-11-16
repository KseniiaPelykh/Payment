using System.Threading.Tasks;

namespace Payment.Core
{
    public interface IPaymentRepository
    {
        Task PutAsync(ValidPaymentData payment, PaymentId id);
        Task<ValidPaymentData> GetAsync(PaymentId id);
    }
}
