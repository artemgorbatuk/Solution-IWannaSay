using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace Client.Web.Controllers;
public class RoomController : Controller {

    private readonly IServiceRoom serviceRoom;

    public RoomController(IServiceRoom serviceRoom) {
        this.serviceRoom = serviceRoom;
    }

    public IActionResult Index() {

        var result = serviceRoom.GetRoomIndexPage();

        return View(result);
    }
}
