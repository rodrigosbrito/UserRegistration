#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["source/WebApi/WebApi.csproj", "source/WebApi/"]
COPY ["source/Application/Application.csproj", "source/Application/"]
COPY ["source/Domain/Domain.csproj", "source/Domain/"]
COPY ["source/Infrastructure/Infrastructure.csproj", "source/Infrastructure/"]
COPY ["source/WebApi.Core/WebApi.Core.csproj", "source/WebApi.Core/"]
RUN dotnet restore "source/WebApi/WebApi.csproj"
COPY . .
WORKDIR "/src/source/WebApi"
RUN dotnet build "WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApi.dll"]