using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace NotebookStoreMVC.Controllers;

[AllowAnonymous]
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

    // GET: /<controller>/Error
    public IActionResult Error()
    {
        return View();
    }
}
