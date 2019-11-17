using System;
using System.Collections.Generic;
using System.Linq;

namespace Payment.Core
{
    public static class Validator
    {
        private static readonly IList<(Rule rule, Predicate<PaymentRequest> isInvalid)> Rules =
            new List<(Rule rule, Predicate<PaymentRequest> isInvalid)>
            {
                (Rule.CardNumberIsRequired, x => string.IsNullOrWhiteSpace(x.CardNumber)),
                (Rule.ExpiryDateIsRequired, x => !x.Expiry.HasValue),
                (Rule.AmountIsRequired, x => x.Amount <= 0),
                (Rule.CurrencyIsRequired, x => string.IsNullOrWhiteSpace(x.Currency)),
                (Rule.CvvIsRequired, x => string.IsNullOrWhiteSpace(x.Cvv))
            };

        public static IReadOnlyCollection<Rule> Validate(PaymentRequest request) =>
            Rules
                .Where(r => r.isInvalid(request))
                .Select(r => r.rule)
                .ToList();
    }
}
