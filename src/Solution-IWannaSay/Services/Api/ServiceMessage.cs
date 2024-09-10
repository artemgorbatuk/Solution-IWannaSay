using Repositories.SignalR.Interfaces;
using Services.Interfaces;

namespace Services.Api;
public class ServiceMessage : IServiceMessage {

    private readonly IConnectorMessage connectorMessage;

    public ServiceMessage(IConnectorMessage connectorMessage)
    {
        this.connectorMessage = connectorMessage;
    }

    public IEnumerable<string> GetMessages() {
        return connectorMessage.GetMessages();
    }

    public async Task SendMessageAsync(string message) {
        await connectorMessage.SendMessageAsync(message);
    }
}