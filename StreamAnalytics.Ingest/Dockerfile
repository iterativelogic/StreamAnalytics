#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["StreamAnalytics.Ingest/StreamAnalytics.Ingest.csproj", "StreamAnalytics.Ingest/"]
RUN dotnet restore "StreamAnalytics.Ingest/StreamAnalytics.Ingest.csproj"
COPY . .
WORKDIR "/src/StreamAnalytics.Ingest"
RUN dotnet build "StreamAnalytics.Ingest.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "StreamAnalytics.Ingest.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StreamAnalytics.Ingest.dll"]