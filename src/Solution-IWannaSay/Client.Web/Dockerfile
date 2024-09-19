FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 5000
ENV ASPNETCORE_HTTP_PORTS=5000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Client.Web/Client.Web.csproj", "Client.Web/"]
COPY ["Services/Services.csproj", "Services/"]
COPY ["Repositories.Ef/Repositories.Ef.csproj", "Repositories.Ef/"]
COPY ["Datasource.Ef/Datasource.Ef.csproj", "Datasource.Ef/"]
COPY ["Repositories.SignalR/Repositories.SignalR.csproj", "Repositories.SignalR/"]
RUN dotnet restore "./Client.Web/Client.Web.csproj"
COPY . .
WORKDIR "/src/Client.Web"
RUN dotnet build "./Client.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Client.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Client.Web.dll"]