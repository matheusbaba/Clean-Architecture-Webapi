# Clean-Architecture-Webapi

This project was built to serve as a reference for new Asp.Net Core WebApis.

## Technologies Used

* C#
* Asp.Net Core 3.1
* Asp.Net Core WebApi
* AutoMapper
* MediatR
* Swagger
* EF Core
* XUnit
* Moq & FakeItEasy

## Design
* SOLID principles
* Domain Driven Design
* Domain Events
* CQRS Pattern
* Specification Pattern
* Unit of Work Pattern

## Solution Layout
```
src
├── Api
├── Domain
├── Infra
└── Tests
    ├── Api.Tests
    ├── Domain.Tests
    └── Infra.Tests
```

## Running the Project

### .NET Core CLI

1. Go to ```src``` folder
2. Run ```dotnet restore```
3. Run ```dotnet build```
4. Run ```dotnet run -p ./CarRentalDDD.API/CarRentalDDD.API.csproj```

### Docker

1. Go to ```src``` folder
2. Run ```docker build -t carrental-aspnetcore .```
3. Run ```docker container run --publish 8000:80 --detach --name carrental carrental-aspnetcore```
4. Access ```http://localhost:8000/```
