# Payment

Open points : 

- PCI data security standart
- authentication
- fraud score
- authorisation and capture 
- lambda with api gateway or ec2 
- PSD2 


## Complex scalable architecture proposal 

![Alt text](https://user-images.githubusercontent.com/4716985/68808916-101fac80-066b-11ea-9695-7fef577ffed3.png)

## Storage

DynamoDB was chosen for storing payment details. 
The main reasons are:
- simple and fast configuration
- cost effective (no/little costs for testing)

For the real business case DynamoDb could be a bad choice because of:
- only AWS deployment
- no predefined schema
- limited data types support

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

Doubts and problematics: 
- Managing storing payment details failure should be improved. In proposed solution inconsistency can be introduced when bank authorize request meanwhile the payment gateway fails to store it to the system.
- Card number mask algorithm should be verified according to standards. 
- CVV and Expiry Date are not stored because it is card detail information and payment gateway could be not authorized to store it.
- Card number can be encrypted 
