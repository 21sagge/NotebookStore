using Microsoft.AspNetCore.Mvc;

namespace NotebookStoreMVC.Controllers;

public class NotebookController : Controller
{
    // GET: /<controller>/
    public IActionResult Index()
    {
        var context = new NotebookStoreContext.NotebookStoreContext();

        var notebookModel = context.Notebooks.ToList();

        ViewData["Notebooks"] = notebookModel;

        return View();
    }
}