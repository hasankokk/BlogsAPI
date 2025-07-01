using Microsoft.AspNetCore.Mvc;

namespace BlogsAPI.Controllers;

public class CommentController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}