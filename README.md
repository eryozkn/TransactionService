
## Transaction Service 
This repo contains Transaction Service with .NET8 Web API and responsible from user balance and transaction CRUD.



**Prerequisities**

.NET8 SDK must be installed



## Build, Test, Run

Run the following commands from the folder containing the .sln file in order to build and test.

`dotnet build`

`dotnet test`



## Local debugging and testing

**Swagger URL** 
`http://localhost:81/swagger/index.html` 

**CURLs**

`
curl --location 'https://localhost:81/api/v1/transaction' \
--header 'Accept: application/json' \
--header 'Content-Type: application/json' \
--data '{
  "userId": 1,
  "amount": 50,
  "currency": "AED",
  "transactionType": 1
}'
`

`
curl --location 'https://localhost:81/api/v1/balance/1'`
