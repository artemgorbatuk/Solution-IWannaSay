# Образ aspnet
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS aspnet

# Образ sdk
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS sdk

#Копируем исходник решения и всех проектов в папку /src
WORKDIR /src
COPY src/Solution-IWannaSay/ .

# Восстанавливаем зависимости
RUN dotnet restore "Solution-IWannaSay.sln"

# Сборка проекта в папку /app/build
FROM sdk AS build

WORKDIR "/src/Client.Web"
RUN dotnet build "Client.Web.csproj" -c Release -o /app/build

WORKDIR "/src/Server.SignalR"
RUN dotnet build "Server.SignalR.csproj" -c Release -o /app/build

# Публикаци проекта в папку /app/publish
FROM build AS publish

WORKDIR "/src/Client.Web"
RUN dotnet publish "Client.Web.csproj" -c Release -o /app/publish/client /p:UseAppHost=false

WORKDIR "/src/Server.SignalR"
RUN dotnet publish "Server.SignalR.csproj" -c Release -o /app/publish/signalr /p:UseAppHost=false

# открытие порта
EXPOSE 8080
EXPOSE 8081

# Финальный этап : копирование всех файлов в корневой каталог.
FROM aspnet AS final
COPY --from=publish /app/publish/client /app/client
COPY --from=publish /app/publish/signalr /app/signalr

# Запуск веб сервиса из папки /app/publish
ENTRYPOINT ["sh", "-c", "dotnet /app/client/Client.Web.dll && dotnet /app/server/Server.SignalR.dll"]

# Очистка места от установочных файлов
RUN apt-get clean && \
    rm -rf /var/lib/apt/lists