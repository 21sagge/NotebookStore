using Microsoft.AspNetCore.Mvc;

namespace NotebookStoreMVC.Controllers;

public class BrandController : Controller
{
    // GET: /<controller>/
    public IActionResult Index()
    {
        var context = new NotebookStoreContext.NotebookStoreContext();

        var brandModel = context.Brands.ToList();

        ViewData["Brands"] = brandModel;

        return View();
    }
}