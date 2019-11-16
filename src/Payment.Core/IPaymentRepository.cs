using System.Threading.Tasks;

namespace Payment.Core
{
    public interface IPaymentRepository
    {
        Task PutAsync(IPayment payment);
        Task<IPayment> GetAsync(PaymentId id);
    }
}
