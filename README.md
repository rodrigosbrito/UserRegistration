 
# .NET 7 Sample User Registration and Authentication with JWT
[![.NET Build](https://github.com/rodrigosbrito/UserRegistration/actions/workflows/build.yml/badge.svg)](https://github.com/rodrigosbrito/UserRegistration/actions/workflows/build.yml)

This is an example of user registration using clean architecture and some principles I learned in the last few days. Additionally, this authentication feature uses JWT in .NET 7 with SQL Server and is connected to the user registration example in my repositories.

The goal is to evolve this code more and more.

## User Registration Workflow
![App Screenshot](https://github.com/rodrigosbrito/UserRegistration/blob/main/User%20Registration.jpg?raw=true)

## Authentication Workflow
![App Screenshot](https://github.com/rodrigosbrito/UserRegistration/blob/main/Authentication.jpg?raw=true)


## Tech Stack  

- Visual Studio 2022
- .NET 7
- C# 11
- Entity Framework Core
- SQL Server
- JWT
- CQRS
- Fail Fast (FluentValidation)
- Result Pattern
- Clean architecture
- TDD - xUnit
- Password with Salt (Security for store password in Database)

