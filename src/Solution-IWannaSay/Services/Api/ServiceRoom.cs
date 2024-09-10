using Repositories.SignalR.Interfaces;
using Services.Interfaces;
using Services.Models;

namespace Services.Api;

/// <summary>
/// Комната состоит из групп (CRUD)
/// </summary>
public class ServiceRoom : IServiceRoom {

    private readonly IConnectorRoom connectorRoom;

    public ServiceRoom(IConnectorRoom connectorRoom)
    {
        this.connectorRoom = connectorRoom;
    }

    public async Task ConnectAsync() {
        await connectorRoom.ConnectAsync();
    }

    public async Task DisconnectAsync() {
        await connectorRoom.DisconnectAsync();
    }

    public RoomIndexPage GetRoomIndexPage() {

        var user1 = new RoomIndexUserModel() {
            Name = "Лев Николаевич Мышкин"
        };
        var user2 = new RoomIndexUserModel() {
            Name = "Настасья Филипповна Барашкова"
        };
        var user3 = new RoomIndexUserModel() {
            Name = "Парфён Семёнович Рогожин"
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
                Text = "Привет!",
                User = user1,
            },
            new RoomIndexMessageModel() {
                Text = "Как твои дела?",
                User= user1,
            },
            new RoomIndexMessageModel() {
                Text = "Привет дорогой!, всё отлично!",
                User = user2,
            },
            new RoomIndexMessageModel() {
                Text = "А Вы кто такой??",
                User = user3,
            },
            new RoomIndexMessageModel() { 
                Text = "Я князь Мышкин",
                User = user1
            },
            new RoomIndexMessageModel() {
                Text = "Сударь, Я вызываю вас на дуэль!",
                User = user3
            },
            new RoomIndexMessageModel() {
                Text = "Господа!, Прошу успокойтесь!",
                User = user2
            }
        };

        var result = new RoomIndexPage() {
            User = user1,
            Groups = groups,
            Messages = messages
        };

        return result;
    }
}