using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace Client.Web.Controllers;

[Authorize]
public class RoomController : Controller {

    private readonly IServiceRoom serviceRoom;
    private readonly IServiceMessage serviceMessage;

    public RoomController(IServiceRoom serviceRoom, IServiceMessage serviceMessage) {
        this.serviceRoom = serviceRoom;
        this.serviceMessage = serviceMessage;
    }

    public IActionResult Index() {
        serviceRoom.ConnectAsync();
        var result = serviceRoom.GetRoomIndexPage();
        return View(result);
    }

    public async Task<PartialViewResult> SendMessage(string message) {

        await serviceMessage.SendMessageAsync(message);

        var messages = serviceMessage.GetMessages();

        var result = serviceRoom.GetRoomIndexPage();
        var user = result.User;
        var new_messages = result.Messages.ToList();
        new_messages.Add(new() { Text = "Тест", User = user });
        result.Messages = new_messages;

        return PartialView("_Messages", result);
    }


    //public PartialViewResult GetMessages() {
    //    var messages = serviceMessage.GetMessages();

    //    var result = serviceRoom.GetRoomIndexPage();
    //    var user = result.User;
    //    var new_messages = result.Messages.ToList();
    //    new_messages.Add(new() { Text = "Тест", User = user });
    //    result.Messages = new_messages;

    //    return PartialView("_Messages", result);
    //}


    //[HttpPost]
    //public async Task<IActionResult> SendMessage(string message) {

    //    await serviceMessage.SendMessageAsync(message);

    //    return RedirectToAction("Index");
    //}
}