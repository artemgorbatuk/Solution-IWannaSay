using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using Repositories.SignalR.Interfaces;

namespace Repositories.SignalR.Api;
public class ConnectorRoom : IConnectorRoom {

    private readonly HubConnection _connection;
    private readonly ILogger<ConnectorMessage> _logger;

    public ConnectorRoom(HubConnection connection, ILogger<ConnectorMessage> logger) {

        _connection = connection;
        _logger = logger;

        _connection.Closed += async (error) => {
            logger.LogInformation($"Connection closed: {error}");
            await _connection.StartAsync();
        };

        _connection.Reconnected += async (connectionId) => {
            logger.LogInformation($"Connection reconnected: {connectionId}");
        };
    }

    public async Task ConnectAsync() {
        await _connection.StartAsync();
        _logger.LogInformation($"Connection started: {_connection.ConnectionId}");
    }

    public async Task DisconnectAsync() {
        await _connection.StopAsync();
        _logger.LogInformation($"Connection stopped: {_connection.ConnectionId}");
    }

    public bool IsConnected() {
        if (_connection == null) {
            return false;
        }

        if (_connection.State == HubConnectionState.Connected) {
            return true;
        }

        return false;
    }
}