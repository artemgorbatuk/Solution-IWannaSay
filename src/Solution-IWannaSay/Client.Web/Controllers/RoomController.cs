using Microsoft.AspNetCore.Mvc;

namespace Client.Web.Controllers;
public class RoomController : Controller {
    public IActionResult Index() {
        return View();
    }
}
