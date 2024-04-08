using Microsoft.AspNetCore.Mvc;

namespace NotebookStoreMVC.Controllers;

public class CpuController : Controller
{
    // GET: /<controller>/
    public IActionResult Index()
    {
        var context = new NotebookStoreContext.NotebookStoreContext();

        var cpuModel = context.Cpus.ToList();

        ViewData["Cpus"] = cpuModel;

        return View();
    }
}