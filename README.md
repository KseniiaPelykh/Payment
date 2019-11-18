# Payment

The solution is divided in API, bank(simulates the acquiring bank), 
database(data storage), core(contains all business logic), and test components.  

## AWS Infrastructure proposal 

![Alt text](https://user-images.githubusercontent.com/4716985/68808916-101fac80-066b-11ea-9695-7fef577ffed3.png)

To guarantee a high scalability the proposed solution can be deployed to EC2 instance accessible by load balancer 
and managed by autoscaling group.
For major security, load balancer should accept the requests only from the merchant, 
anyway the authentication is required between merchant and payment gateway.

## Storage

DynamoDB was chosen for storing payment details. 
The main reasons are:
- simple and fast configuration
- cost effective (no/little costs for testing)

For the real business case DynamoDb could be a bad choice because of:
- only AWS deployment
- no predefined schema
- limited data type support

This properties are stored: 
- Amount: amount
- BankAuthorizationId: unique identifier returned by bank
- CardNumber: masked card number 
- OperationDate: the date of payment request arrival
- PaymentId: unique identifier generated by system
- Success: the result of payment 

**Success example:**
![Alt_text](https://user-images.githubusercontent.com/4716985/68995147-97973680-088a-11ea-9171-ab60b2e1bf2e.png)


**Failure example:**
![Alt_text](https://user-images.githubusercontent.com/4716985/68995125-7afafe80-088a-11ea-928c-b3b5723f0c4a.png)

**Storage Improvements:** 
- Managing storing payment details failure should be improved. In proposed solution inconsistency can be introduced when bank authorizes request meanwhile the payment gateway fails to store it to the system.
- Card number can be encrypted and mask algorithm should be verified according to standards. 
- CVV and Expiry Date are not stored because it is card detail information and payment gateway could be not authorized to store it.
- Integration test for DynamoDB

## Other Improvements:
1. Authentication should be introduced between entities
2. Card details should be encrypted
3. Logging
4. Integration testing
5. Performance testing
6. Currency should have predefined values
