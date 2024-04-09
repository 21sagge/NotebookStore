using Microsoft.AspNetCore.Mvc;

namespace NotebookStoreMVC.Controllers;

public class MemoryController : Controller
{
    // GET: /<controller>/
    public IActionResult Index()
    {
        var context = new NotebookStoreContext.NotebookStoreContext();

        var memoryModel = context.Memories.ToList();

        ViewData["Memories"] = memoryModel;

        return View();
    }
}