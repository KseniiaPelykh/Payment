namespace Payment.Core
{
    public class BankAuthorizationId
    {
        public string Value { get; }

        public BankAuthorizationId(string value) => Value = value;
    }
}
