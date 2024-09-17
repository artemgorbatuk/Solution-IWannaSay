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

WORKDIR "/src/Datasource.SignalR"
RUN dotnet build "Datasource.SignalR.csproj" -c Release -o /app/build

# Публикаци проекта в папку /app/publish
FROM build AS publish

WORKDIR "/src/Client.Web"
RUN dotnet publish "Client.Web.csproj" -c Release -o /app/publish/client /p:UseAppHost=false

WORKDIR "/src/Datasource.SignalR"
RUN dotnet publish "Datasource.SignalR.csproj" -c Release -o /app/publish/signalr /p:UseAppHost=false

# открытие порта
EXPOSE 5000
EXPOSE 6000

# Финальный этап : копирование всех файлов в корневой каталог.
FROM aspnet AS final
COPY --from=publish /app/publish/client /app/client
COPY --from=publish /app/publish/signalr /app/signalr

# Вероятно supervisord это слишком и есть смысл использовать вместо него ENTRYPOINT 
# Запуск веб сервиса из папки /app/publish
# ENTRYPOINT ["sh", "-c", "dotnet /app/client/Client.Web.dll & dotnet /app/server/Server.SignalR.dll"]

# Установка пакетов и запуск двух процессов
RUN apt-get update && apt-get install -y supervisor

# Создаем конфигурацию для supervisord
RUN echo "[supervisord]" > /etc/supervisor/conf.d/supervisord.conf && \
    echo "nodaemon=true" >> /etc/supervisor/conf.d/supervisord.conf && \
    echo "user=root" >> /etc/supervisor/conf.d/supervisord.conf && \
    echo "[unix_http_server]" >> /etc/supervisor/conf.d/supervisord.conf && \
    echo "file=/tmp/supervisor.sock" >> /etc/supervisor/conf.d/supervisord.conf && \
    echo "chmod=0700" >> /etc/supervisor/conf.d/supervisord.conf && \
    echo "username=admin" >> /etc/supervisor/conf.d/supervisord.conf && \
    echo "password=admin_pass" >> /etc/supervisor/conf.d/supervisord.conf && \
    echo "[program:client_web]" >> /etc/supervisor/conf.d/supervisord.conf && \
    echo "command=dotnet /app/client/Client.Web.dll --urls \"http://*:5000\"" >> /etc/supervisor/conf.d/supervisord.conf && \
    echo "autostart=true" >> /etc/supervisor/conf.d/supervisord.conf && \
    echo "autorestart=true" >> /etc/supervisor/conf.d/supervisord.conf && \
    echo "stderr_logfile=/var/log/client_web.err.log" >> /etc/supervisor/conf.d/supervisord.conf && \
    echo "stdout_logfile=/var/log/client_web.out.log" >> /etc/supervisor/conf.d/supervisord.conf && \
    echo "[program:signalr]" >> /etc/supervisor/conf.d/supervisord.conf && \
    echo "command=dotnet /app/signalr/Datasource.SignalR.dll --urls \"http://*:6000\"" >> /etc/supervisor/conf.d/supervisord.conf && \
    echo "autostart=true" >> /etc/supervisor/conf.d/supervisord.conf && \
    echo "autorestart=true" >> /etc/supervisor/conf.d/supervisord.conf && \
    echo "stderr_logfile=/var/log/signalr.err.log" >> /etc/supervisor/conf.d/supervisord.conf && \
    echo "stdout_logfile=/var/log/signalr.out.log" >> /etc/supervisor/conf.d/supervisord.conf

# Запуск supervisord для управления несколькими процессами
CMD ["/usr/bin/supervisord"]

# Очистка места от установочных файлов
RUN apt-get clean && \
    rm -rf /var/lib/apt/lists