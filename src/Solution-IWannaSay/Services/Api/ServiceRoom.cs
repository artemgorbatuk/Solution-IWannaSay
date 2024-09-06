using Services.Interfaces;
using Services.Models;

namespace Services.Api;
public class ServiceRoom : IServiceRoom {
    public RoomIndexPage GetRoomIndexPage() {

        var user = new RoomIndexUserModel() {
            Name = "Лев Николаевич Мышкин"
        };

        var groups = new List<RoomIndexGroupModel>() {
            new RoomIndexGroupModel {
                Name = "Group 01"
            },
            new RoomIndexGroupModel {
                Name = "Group 02"
            },
            new RoomIndexGroupModel {
                Name = "Group 03"
            },
        };

        var messages = new List<RoomIndexMessageModel>() {
            new RoomIndexMessageModel() {
                Text = "Hello!"
            }
        };

        var result = new RoomIndexPage() {
            User = user,
            Groups = groups,
            Messages = messages
        };

        return result;
    }
}