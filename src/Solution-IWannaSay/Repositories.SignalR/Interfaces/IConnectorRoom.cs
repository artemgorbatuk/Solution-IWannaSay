namespace Repositories.SignalR.Interfaces;
public interface IConnectorRoom {
    Task ConnectAsync();
    Task DisconnectAsync();
}