using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using Repositories.SignalR.Interfaces;
using System.Collections.Concurrent;

namespace Repositories.SignalR.Api;
public class ConnectorMessage : IConnectorMessage {

    private readonly HubConnection _connection;
    private readonly ILogger<ConnectorMessage> _logger;

    public ConcurrentBag<string> _messages;
    public ConnectorMessage(HubConnection connection, ILogger<ConnectorMessage> logger) {
        _connection = connection;
        _logger = logger;

        _messages = new ConcurrentBag<string>();

        _connection.On("ReceiveMessage", (string message) => {
            _messages.Add(message);
            //_logger.LogInformation($"Connection: {_connection.ConnectionId}, receive message: {message}");
        });
    }

    public IEnumerable<string> GetMessages() {
        return _messages;
    }

    public async Task SendMessageAsync(string message) {
        await _connection.InvokeAsync("SendMessage", message);
        _logger.LogInformation($"Connection: {_connection.ConnectionId}, send message: {message}");
    }
}