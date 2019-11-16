provider "aws" {}

resource "aws_dynamodb_table" "payments-dynamodb-table" {
  name           = "Payments"
  billing_mode   = "PROVISIONED"
  read_capacity  = 1
  write_capacity = 1
  hash_key       = "PaymentId"

  attribute {
    name = "PaymentId"
    type = "S"
  }

  tags = {
    Name = "payment"
  }
}
