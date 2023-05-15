#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["source/UserRegistration.WebApi/UserRegistration.WebApi.csproj", "source/UserRegistration.WebApi/"]
COPY ["source/UserRegistration.Application/UserRegistration.Application.csproj", "source/UserRegistration.Application/"]
COPY ["source/UserRegistration.Domain/UserRegistration.Domain.csproj", "source/UserRegistration.Domain/"]
COPY ["source/UserRegistration.Infrastructure/UserRegistration.Infrastructure.csproj", "source/UserRegistration.Infrastructure/"]
COPY ["source/UserRegistration.WebApi.Core/UserRegistration.WebApi.Core.csproj", "source/UserRegistration.WebApi.Core/"]
RUN dotnet restore "source/UserRegistration.WebApi/UserRegistration.WebApi.csproj"
COPY . .
WORKDIR "/src/source/UserRegistration.WebApi"
RUN dotnet build "UserRegistration.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UserRegistration.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UserRegistration.WebApi.dll"]