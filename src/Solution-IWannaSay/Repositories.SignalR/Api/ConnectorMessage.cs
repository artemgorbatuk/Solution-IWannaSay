using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using Repositories.SignalR.Interfaces;

namespace Repositories.SignalR.Api;
public class ConnectorMessage : IConnectorMessage {

    private readonly HubConnection _connection;
    private readonly ILogger<ConnectorMessage> _logger;

    public ConnectorMessage(HubConnection connection, ILogger<ConnectorMessage> logger) {
        _connection = connection;
        _logger = logger;
    }

    public IEnumerable<string> GetMessages() {
        var messages = new List<string>();
        _connection.On("ReceiveMessage", (string message) => {
            messages.Add(message);
            _logger.LogInformation($"Connection: {_connection.ConnectionId}, receive message: {message}");
        });
        return messages;
    }

    public async Task SendMessageAsync(string message) {
        await _connection.InvokeAsync("SendMessage", message);
        _logger.LogInformation($"Connection: {_connection.ConnectionId}, send message: {message}");
    }
}