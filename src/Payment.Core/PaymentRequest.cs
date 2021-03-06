﻿using System;

namespace Payment.Core
{
    public class PaymentRequest
    {
        public string CardNumber { get; }
        public DateTimeOffset? Expiry { get; }
        public decimal Amount { get; }
        public string Currency { get; }
        public string Cvv { get; }

        public PaymentRequest(
            string cardNumber, 
            DateTimeOffset? expiry, 
            decimal amount, 
            string currency, 
            string cvv)
        {
            CardNumber = cardNumber;
            Expiry = expiry;
            Amount = amount;
            Currency = currency;
            Cvv = cvv;
        }
    }
}
