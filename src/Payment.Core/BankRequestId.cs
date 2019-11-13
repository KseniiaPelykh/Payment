namespace Payment.Core
{
    public class BankRequestId
    {
        public string Value { get; }

        public BankRequestId(string value) => Value = value;
    }
}
