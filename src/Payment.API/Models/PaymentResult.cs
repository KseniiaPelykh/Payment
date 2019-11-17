namespace Payment.API.Models
{
    public class PaymentResult
    {
        public string  Id { get; }

        public Status Status { get; }

        public PaymentResult(string id, Status status)
        {
            Id = id;
            Status = status;
        }
    }
}
