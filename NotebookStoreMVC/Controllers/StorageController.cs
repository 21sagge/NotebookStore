using Microsoft.AspNetCore.Mvc;

namespace NotebookStoreMVC.Controllers;

public class StorageController : Controller
{
    // GET: /<controller>/
    public IActionResult Index()
    {
        var context = new NotebookStoreContext.NotebookStoreContext();

        var storageModel = context.Storages.ToList();

        ViewData["Storages"] = storageModel;

        return View();
    }
}