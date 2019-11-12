namespace Payment.Core
{
    public class BankResponse
    {
        public Identifier Identifier { get; }
        public bool IsSuccess { get; }

        private BankResponse(Identifier identifier, bool success)
        {
            Identifier = identifier;
            IsSuccess = success;
        }

        public static BankResponse CreateSuccess(Identifier identifier)
        {
            return new BankResponse(identifier, true);
        }

        public static BankResponse CreateFailure()
        {
            return new BankResponse(null, false);
        }
    }
}
