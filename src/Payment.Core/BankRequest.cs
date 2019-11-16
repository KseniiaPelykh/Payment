namespace Payment.Core
{
    public class BankRequest
    {
       public string Data { get; }

        public BankRequest(string data)
        {
            Data = data;
        }
    }
}
