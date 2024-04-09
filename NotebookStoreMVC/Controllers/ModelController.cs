using Microsoft.AspNetCore.Mvc;

namespace NotebookStoreMVC.Controllers;

public class ModelController : Controller
{
    // GET: /<controller>/
    public IActionResult Index()
    {
        var context = new NotebookStoreContext.NotebookStoreContext();

        var modelModel = context.Models.ToList();

        ViewData["Models"] = modelModel;

        return View();
    }
}