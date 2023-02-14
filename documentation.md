# Documentation

## launching
1. Go to [Meetup.API](Meetup/Meetup.API) directory.
2. Open command line
3. Enter `dotnet run`

## The first Steps
StartPath: https://localhost:7132/api

#### 1. Create account(speaker):
If you prefer Swagger => https://localhost:7132/swagger/index.html
If you prefer Postman =>
Method: `POST`
url: `$"{StartPath}/speaker"`
Body: `{
  "name": "string",
  "email": "string",
  "password": "string"}`
#### 2. Get Token
Method: `POST`
url:`$"{StartPath}/token"`
Body: `{  
  "email": "string",
  "password": "string"}`

## docs
StartPath: https://localhost:7132/api

#### 1. Return tokens
Method: `POST`
url:`$"{StartPath}/token"`
Body: `{  
  "email": "string",
  "password": "string"}`

#### 2. Refresh tokens
Method: `POST`
url:`$"{StartPath}/token"`
Body: `{  
  "accessToken": "string",
  "refreshToken": "string"}`

#### 3. Create speaker
Method: `POST`
url:`$"{StartPath}/speaker"`
Body: `{  
  "Name": "string",
  "Email": "string",
  "Password": "string",}`

#### 4. Get speakers
Method: `GET`
url:`$"{StartPath}/speaker"`

#### 5. Get speaker by Id
Method: `GET`
url:`$"{StartPath}/speaker/{id}"`

#### 6. Update speaker
Method: `UPDATE`
url:`$"{StartPath}/speaker/{id}"`
Body: `{  
  "Name": "string",
  "Email": "string",
  "Password": "string",}`

#### 7. Delete speaker
Method: `Delete`
url:`$"{StartPath}/speaker/{id}"`

#### 8. Create event
Method: `POST`
url:`$"{StartPath}/event"`
Body: `{  
  "title": "string",
  "description": "string",
  "location": "string",
  "speakerId": "int32",
  "date": "string(date-time)"}`

#### 9. Get events
Method: `GET`
url:`$"{StartPath}/event"`

#### 5. Get eveny by Id
Method: `GET`
url:`$"{StartPath}/event/{id}"`

#### 6. Update event
Method: `UPDATE`
url:`$"{StartPath}/speaker/{id}"`
Body: `{  
  "title": "string",
  "description": "string",
  "location": "string",
  "speakerId": "int32",
  "date": "string(date-time)"}`

#### 7. Delete event
Method: `Delete`
url:`$"{StartPath}/event/{id}"`