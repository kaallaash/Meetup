# Documentation

### launching
1. Go to [Meetup.API](Meetup/Meetup.API) directory.
2. Open command line
3. Enter `dotnet run`

### The first Steps
StartPath: https://localhost:7132/api

1) Create account(speaker):
If you prefer Swagger => https://localhost:7132/swagger/index.html
If you prefer Postman =>
Method: `POST`
url: `$"{StartPath}/speaker"`
Body: `{
  "name": "string",
  "email": "string",
  "password": "string"}`
2) Get Token
Method: `POST`
url:`$"{StartPath}/token"`
Body: `{  
  "email": "string",
  "password": "string"}`

### docs
StartPath: https://localhost:7132/api

1) Return tokens
Method: `POST`
url:`$"{StartPath}/token"`
Body: `{  
  "email": "string",
  "password": "string"}`

2) Refresh tokens
Method: `POST`
url:`$"{StartPath}/token"`
Body: `{  
  "accessToken": "string",
  "refreshToken": "string"}`