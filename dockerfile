# DotNet
FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine AS dotnet
COPY source ./source
RUN dotnet publish ./source/WebApi/WebApi.csproj --configuration Release --output /out

# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine
RUN apk add --no-cache icu-libs
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
ENV ASPNETCORE_ENVIRONMENT="Development"
EXPOSE 80
WORKDIR /app
COPY --from=dotnet /out .
ENTRYPOINT ["dotnet", "WebApi.dll"]