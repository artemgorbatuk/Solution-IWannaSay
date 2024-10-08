﻿using Microsoft.AspNetCore.SignalR.Client;
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
            await Task.CompletedTask;
        };

        _connection.Reconnected += async (connectionId) => {
            logger.LogInformation($"Connection reconnected: {connectionId}");
            await Task.CompletedTask;
        };
    }

    public async Task ConnectAsync() {
        await _connection.StartAsync();
        _logger.LogInformation($"Connection started: {_connection.ConnectionId}");
    }

    public async Task DisconnectAsync() {
        var connectionId = _connection.ConnectionId;
        await _connection.StopAsync();
        _logger.LogInformation($"Connection stopped: {connectionId}");
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