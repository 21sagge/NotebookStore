using Microsoft.AspNetCore.Mvc;

namespace NotebookStoreMVC.Controllers;

public class DisplayController : Controller
{
    // GET: /<controller>/
    public IActionResult Index()
    {
        var context = new NotebookStoreContext.NotebookStoreContext();

        var displayModel = context.Displays.ToList();

        ViewData["Displays"] = displayModel;

        return View();
    }
}