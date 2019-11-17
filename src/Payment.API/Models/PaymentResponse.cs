using System.Collections.Generic;
using Payment.Core;
using Payment.Core.Utility;

namespace Payment.API.Models
{
    public class PaymentResponse
    {
        public string  Id { get; }

        public Status Status { get; }

        public IReadOnlyCollection<string> Errors { get; }

        public PaymentResponse(Result<PaymentId> result)
        {
            var paymentId = result.Value;
            Id = paymentId.Value;
            Status = result.IsSuccess ? Status.Success : Status.Failure;
            Errors = new List<string>();
        }

        public PaymentResponse(IReadOnlyCollection<string> errors)
        {
            Status = Status.Failure;
            Errors = errors;
        }
    }
}
