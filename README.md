 
# .NET 7 Sample User Registration and Authentication with JWT
[![.NET Build](https://github.com/rodrigosbrito/UserRegistration/actions/workflows/build.yml/badge.svg)](https://github.com/rodrigosbrito/UserRegistration/actions/workflows/build.yml)

This is an example of user registration using clean architecture and some principles I learned in the last few days. Additionally, this authentication feature uses JWT in .NET 7 with SQL Server and is connected to the user registration example in my repositories.

The goal is to evolve this code more and more.

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
- Serilog
- Observability with Elastic APM (ElasticSearch and Kibana)

## User Registration Workflow
![App Screenshot](https://github.com/rodrigosbrito/UserRegistration/blob/main/docs/User%20Registration.jpg?raw=true)

## Authentication Workflow
![App Screenshot](https://github.com/rodrigosbrito/UserRegistration/blob/main/docs/Authentication.jpg?raw=true)

## A little bit of Observability
![App Screenshot](https://github.com/rodrigosbrito/UserRegistration/blob/main/docs/elastic_apm_containers.PNG?raw=true)

The image displayed above illustrates the containerization of Elastic APM, an application performance monitoring tool that offers real-time performance insights for applications. Elasticsearch, a distributed search and analytics engine, facilitates efficient storage and search of large datasets. Kibana, a data visualization tool, provides comprehensive data exploration and analysis features.

All three services were instantiated using Docker Compose. The following command can be used to launch the containers from the root directory of the docker-compose.yml file:
 
~~~docker  
  docker-compose up --build -t
~~~  
![App Screenshot](https://github.com/rodrigosbrito/UserRegistration/blob/main/docs/elastic_apm_services.png?raw=true)
