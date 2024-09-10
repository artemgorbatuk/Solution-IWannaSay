using Services.Models;

namespace Services.Interfaces;
public interface IServiceRoom {
    RoomIndexPage GetRoomIndexPage();
    Task ConnectAsync();
    Task DisconnectAsync();
}
