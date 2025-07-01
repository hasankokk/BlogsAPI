using Microsoft.AspNetCore.Mvc;

namespace BlogsAPI.Controllers;

public class AdminController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}