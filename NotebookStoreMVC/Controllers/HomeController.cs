using Microsoft.AspNetCore.Mvc;

namespace NotebookStoreMVC.Controllers;

public class HomeController : Controller
{
    // GET: /<controller>/
    public IActionResult Index()
    {
        return View();
    }

    // GET: /<controller>/Privacy
    public IActionResult Privacy()
    {
        return View();
    }
}
