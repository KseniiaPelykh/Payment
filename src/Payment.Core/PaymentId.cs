using System;

namespace Payment.Core
{
    public class PaymentId
    {
        public string Value { get; }

        public PaymentId(string value) =>
            Value = value;

        public static PaymentId GenerateNew() =>
            new PaymentId(Guid.NewGuid().ToString("N"));
    }
}
