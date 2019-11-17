using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Payment.Core.Tests
{
    public class ValidatorTests
    {
        private static IEnumerable<PaymentRequest> InvalidRequests
        {
            get
            {
                yield return new PaymentRequest(string.Empty, DateTimeOffset.UtcNow, 100, "EUR", "123");
                yield return new PaymentRequest("0000-0000-0000-0000", DateTimeOffset.UtcNow, 100, string.Empty, "123");
                yield return new PaymentRequest("0000-0000-0000-0000", null, 100, "EUR", "123");
                yield return new PaymentRequest("0000-0000-0000-0000", DateTimeOffset.UtcNow, -100, "EUR", "123");
                yield return new PaymentRequest("0000-0000-0000-0000", DateTimeOffset.UtcNow, 100, "EUR", null);
                yield return new PaymentRequest(string.Empty, null, -100, "EUR", "123");
            }
        }

        private static IEnumerable<PaymentRequest> ValidRequests
        {
            get
            {
                yield return new PaymentRequest("0000-0000-0000-0000", DateTimeOffset.UtcNow, 100, "EUR", "123");
                yield return new PaymentRequest("0000-0000-0000-0000", DateTimeOffset.UtcNow, 100.25M, "EUR", "000");
            }
        }

        public static IEnumerable<object[]> InvalidTestParameters =>
            InvalidRequests.Select(x => new object[] { x });

        public static IEnumerable<object[]> ValidTestParameters =>
            ValidRequests.Select(x => new object[] { x });

        [Theory]
        [MemberData(nameof(InvalidTestParameters))]
        public void ShouldBeInvalid(PaymentRequest request)
        {
            Assert.True(Validator.Validate(request).Any());
        }

        [Theory]
        [MemberData(nameof(ValidTestParameters))]
        public void ShouldBeValid(PaymentRequest request)
        {
            Assert.False(Validator.Validate(request).Any());
        }
    }
}
