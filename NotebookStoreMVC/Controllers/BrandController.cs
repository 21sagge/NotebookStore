using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NotebookStoreMVC.Models;
using NotebookStore.Business;
using Microsoft.AspNetCore.Authorization;

namespace NotebookStoreMVC.Controllers;

[Authorize(Roles = "Admin,Editor")]
public class BrandController : Controller
{
    private readonly IMapper mapper;
    private readonly IServices services;

    public BrandController(IMapper mapper, IServices services)
    {
        this.mapper = mapper;
        this.services = services;
    }

    // GET: BrandViewModel
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var brands = await services.Brands.GetAll();
        var mappedBrands = mapper.Map<IEnumerable<BrandViewModel>>(brands);

        return View(mappedBrands);
    }

    // GET: BrandViewModel/Details/5
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var brand = await services.Brands.Find(id);

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
        ModelState.Remove("CreatedAt");

        if (ModelState.IsValid)
        {
            await services.Brands.Create(mapper.Map<BrandDto>(BrandViewModel));

            return RedirectToAction(nameof(Index));
        }

        return View(BrandViewModel);
    }

    // GET: BrandViewModel/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        var brand = await services.Brands.Find(id);

        if (brand == null)
        {
            return NotFound();
        }

        return View(mapper.Map<BrandViewModel>(brand));
    }

    // POST: BrandViewModel/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditAsync(int id, [Bind("Id,Name,CreatedBy,CreatedAt")] BrandViewModel BrandViewModel)
    {
        if (id != BrandViewModel.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            var result = await services.Brands.Update(mapper.Map<BrandDto>(BrandViewModel));

            if (result)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError("", "Unauthorized");
            }
        }

        return View(BrandViewModel);
    }

    // GET: BrandViewModel/Delete/5
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        if (services.Brands.GetAll() == null)
        {
            return NotFound();
        }

        var brand = await services.Brands.Find(id);

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
        await services.Brands.Delete(id);

        return RedirectToAction(nameof(Index));
    }
}
