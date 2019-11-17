namespace Payment.Core
{
    public enum Rule
    {
        CardNumberIsRequired,
        ExpiryDateIsRequired,
        AmountIsRequired,
        CurrencyIsRequired,
        CvvIsRequired
    }
}
