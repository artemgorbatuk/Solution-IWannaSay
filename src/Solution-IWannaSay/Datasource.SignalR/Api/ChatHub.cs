using Microsoft.AspNetCore.SignalR;
namespace Datasource.SignalR.Api;

public class ChatHub : Hub {

    private readonly ILogger _logger;

    public ChatHub(ILogger<ChatHub> logger) {
        _logger = logger;
    }

    public async Task SendMessage(string message) {
        await Clients.All.SendAsync("ReceiveMessage", message);
        _logger.LogInformation($"Клиент: {Context.ConnectionId} , сообщение: {message}.");
    }

    public override async Task OnConnectedAsync() {
        _logger.LogInformation($"Клиент подключился: {Context.ConnectionId}");
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception) {
        _logger.LogInformation($"Клиент отключился: {Context.ConnectionId}");
        await base.OnDisconnectedAsync(exception);
    }
}