FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /source

COPY *.slnx .
COPY src/TimescaleDataProcessor.Api/*.csproj ./src/TimescaleDataProcessor.Api/
RUN dotnet restore

COPY src/ ./src/
WORKDIR ./src/TimescaleDataProcessor.Api
RUN dotnet publish -c Release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app ./

RUN apt-get update && apt-get install -y curl

ENTRYPOINT ["dotnet", "TimescaleDataProcessor.Api.dll"]
