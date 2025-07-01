using Microsoft.AspNetCore.Mvc;

namespace BlogsAPI.Controllers;

public class PostController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}