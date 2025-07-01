using Microsoft.AspNetCore.Mvc;

namespace BlogsAPI.Controllers;

public class ReportController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}