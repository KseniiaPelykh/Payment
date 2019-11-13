namespace Payment.Core
{
    public class PaymentId
    {
        public string Value { get; }

        public PaymentId(string value) =>
            Value = value;
    }
}
