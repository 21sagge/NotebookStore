using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    // GET: Brand/Delete/
    public async Task<IActionResult> Delete(int? Id)
    {
        var context = new NotebookStoreContext.NotebookStoreContext();

        if (Id == null)
        {
            return NotFound();
        }

        var brand = await context.Brands
            .FirstOrDefaultAsync(b => b.Id == Id);
        if (brand == null)
        {
            return NotFound();
        }

        return View(brand);
    }

    // POST: Movies/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var context = new NotebookStoreContext.NotebookStoreContext();
        var brand = await context.Brands.FindAsync(id);
        if (brand != null)
        {
            context.Brands.Remove(brand);
        }

        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }



    
}