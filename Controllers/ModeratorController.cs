using Microsoft.AspNetCore.Mvc;

namespace BlogsAPI.Controllers;

public class ModeratorController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}