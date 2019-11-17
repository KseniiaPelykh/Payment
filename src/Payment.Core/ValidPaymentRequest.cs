using System;
using System.Linq;
using Payment.Core.Utility;

namespace Payment.Core
{
    public class ValidPaymentRequest
    {
        public string CardNumber { get; }
        public DateTimeOffset Expiry { get; }
        public decimal Amount { get; }
        public string Currency { get; }
        public string Cvv { get; }

        private ValidPaymentRequest(PaymentRequest request)
        {
            CardNumber = request.CardNumber;
            Expiry = request.Expiry.Value;
            Amount = request.Amount;
            Currency = request.Currency;
            Cvv = request.Cvv;
        }

        public static Result<ValidPaymentRequest> Create(PaymentRequest request)
        {
            var errors = Validator.Validate(request);
            return !errors.Any()
                ? Result<ValidPaymentRequest>.CreateSuccess(new ValidPaymentRequest(request))
                : Result<ValidPaymentRequest>.CreateFailure(errors.Select(e => e.ToString()).ToList());
        }
    }
}
