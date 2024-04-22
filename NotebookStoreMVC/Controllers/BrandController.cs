using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NotebookStoreMVC.Models;
using NotebookStore.Business;

namespace NotebookStoreMVC.Controllers;

public class BrandController : Controller
{
    private readonly IMapper mapper;
    private readonly BrandService service;

    public BrandController(IMapper mapper, BrandService service)
    {
        this.mapper = mapper;
        this.service = service;
    }

    // GET: BrandViewModel
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var brands = await service.GetBrands();
        var mappedBrands = mapper.Map<IEnumerable<BrandViewModel>>(brands);

        return View(mappedBrands);
    }

    // GET: BrandViewModel/Details/5
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        if (service.GetBrands() == null)
        {
            return NotFound();
        }

        var brand = await service.GetBrand(id);

        if (brand == null)
        {
            return NotFound();
        }

        return View(mapper.Map<BrandViewModel>(brand));
    }

    // GET: BrandViewModel/Create
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    // POST: BrandViewModel/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateAsync([Bind("Id,Name")] BrandViewModel BrandViewModel)
    {
        if (ModelState.IsValid)
        {
            await service.CreateBrand(mapper.Map<BrandDto>(BrandViewModel));

            return RedirectToAction(nameof(Index));
        }

        return View(BrandViewModel);
    }

    // GET: BrandViewModel/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        if (service.GetBrands() == null)
        {
            return NotFound();
        }

        var brand = await service.GetBrand(id);

        if (brand == null)
        {
            return NotFound();
        }

        return View(mapper.Map<BrandViewModel>(brand));
    }

    // POST: BrandViewModel/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditAsync(int id, [Bind("Id,Name")] BrandViewModel BrandViewModel)
    {
        if (id != BrandViewModel.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                await service.UpdateBrand(mapper.Map<BrandDto>(BrandViewModel));
            }
            catch (Exception)
            {
                return Problem("Update failed.");
            }

            return RedirectToAction(nameof(Index));
        }

        return View(BrandViewModel);
    }

    // GET: BrandViewModel/Delete/5
    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        if (service.GetBrands() == null)
        {
            return NotFound();
        }

        var brand = await service.GetBrand(id);

        if (brand == null)
        {
            return NotFound();
        }

        return View(mapper.Map<BrandViewModel>(brand));
    }

    // POST: BrandViewModel/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (service.GetBrands() == null)
        {
            return Problem("Entity set 'NotebookStoreContext.Brands'  is null.");
        }

        await service.DeleteBrand(id);

        return RedirectToAction(nameof(Index));
    }

    private async Task<bool> BrandExists(int id)
    {
        return await service.BrandExists(id);
    }
}
