using Amazon.DynamoDBv2.DataModel;

namespace Payment.DatabaseAdapter
{
    [DynamoDBTable("Payments")]
    public class Payment 
    {
        [DynamoDBHashKey]
        public string PaymentId { get; set; }

        [DynamoDBProperty]
        public string CardNumber { get; set; }

        [DynamoDBProperty]
        public decimal Amount { get; set; }

        [DynamoDBProperty]
        public string Currency { get; set; }

        [DynamoDBProperty]
        public string OperationDate { get; set; }

        [DynamoDBProperty]
        public bool Success { get; set; }

        [DynamoDBProperty]
        public string BankAuthorizationId { get; set; }
    }
}
