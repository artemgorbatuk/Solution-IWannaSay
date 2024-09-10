namespace Repositories.SignalR.Interfaces;
public interface IConnectorMessage {
    Task SendMessageAsync(string message);
    IEnumerable<string> GetMessages();
}