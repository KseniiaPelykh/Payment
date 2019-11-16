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


