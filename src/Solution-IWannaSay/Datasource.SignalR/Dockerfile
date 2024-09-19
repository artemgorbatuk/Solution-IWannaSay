FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 6000
ENV ASPNETCORE_HTTP_PORTS=6000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Datasource.SignalR/Datasource.SignalR.csproj", "Datasource.SignalR/"]
RUN dotnet restore "./Datasource.SignalR/Datasource.SignalR.csproj"
COPY . .
WORKDIR "/src/Datasource.SignalR"
RUN dotnet build "./Datasource.SignalR.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Datasource.SignalR.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Datasource.SignalR.dll"]