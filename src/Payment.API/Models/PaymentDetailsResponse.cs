using System;
using Payment.Core;

namespace Payment.API.Models
{
    public class PaymentDetailsResponse
    {
        public string Id { get; }
        public  string CardNumber { get; }
        public decimal Amount { get; }
        public string Currency { get; }
        public DateTimeOffset OperationDate { get; }
        public bool Successful { get; }

        public PaymentDetailsResponse(IPayment payment)
        {
            Id = payment.PaymentId.Value;
            CardNumber = payment.CardNumber;
            Amount = payment.Amount;
            Currency = payment.Currency;
            OperationDate = payment.OperationDate;
            Successful = payment.BankAuthorizationResult.IsSuccess;
        }
    }
}
