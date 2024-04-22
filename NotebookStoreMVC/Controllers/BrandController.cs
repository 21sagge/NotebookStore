using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NotebookStoreMVC.Models;
using NotebookStore.DAL;
using NotebookStore.Entities;

namespace NotebookStoreMVC.Controllers;

public class BrandController : Controller
{
    private readonly IMapper mapper;
    private readonly IUnitOfWork unitOfWork;

    public BrandController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        this.mapper = mapper;
        this.unitOfWork = unitOfWork;
    }

    // GET: BrandViewModel
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var brands = await unitOfWork.Brands.Read();

        return View(mapper.Map<IEnumerable<BrandViewModel>>(brands));
    }

    // GET: BrandViewModel/Details/5
    [HttpGet]
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || unitOfWork.Brands.Read() == null)
        {
            return NotFound();
        }

        var brand = await unitOfWork.Brands.Find(id);

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
    public IActionResult Create([Bind("Id,Name")] BrandViewModel BrandViewModel)
    {
        unitOfWork.BeginTransaction();

        try
        {
            unitOfWork.Brands.Create(mapper.Map<Brand>(BrandViewModel));
            unitOfWork.SaveAsync();
            unitOfWork.CommitTransaction();
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            unitOfWork.RollbackTransaction();
            return Problem(ex.Message);
        }
    }

    // GET: BrandViewModel/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || unitOfWork.Brands.Read() == null)
        {
            return NotFound();
        }

        var brand = await unitOfWork.Brands.Find(id);

        if (brand == null)
        {
            return NotFound();
        }

        return View(mapper.Map<BrandViewModel>(brand));
    }

    // POST: BrandViewModel/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, [Bind("Id,Name")] BrandViewModel BrandViewModel)
    {
        if (id != BrandViewModel.Id)
        {
            return NotFound();
        }

        unitOfWork.BeginTransaction();

        try
        {
            unitOfWork.Brands.Update(mapper.Map<Brand>(BrandViewModel));
            unitOfWork.SaveAsync();
            unitOfWork.CommitTransaction();

            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            unitOfWork.RollbackTransaction();
            return Problem(ex.Message);
        }
    }

    // GET: BrandViewModel/Delete/5
    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || unitOfWork.Brands.Read() == null)
        {
            return NotFound();
        }

        var brand = await unitOfWork.Brands.Find(id);

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
        if (unitOfWork.Brands.Read() == null)
        {
            return Problem("Entity set 'NotebookStoreContext.Brands'  is null.");
        }

        unitOfWork.BeginTransaction();

        try
        {
            if (await unitOfWork.Brands.Find(id) == null)
            {
                return NotFound();
            }

            await unitOfWork.Brands.Delete(id);

            unitOfWork.CommitTransaction();

            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            unitOfWork.RollbackTransaction();
            return Problem(ex.Message);
        }
    }

    private bool BrandExists(int id)
    {
        return unitOfWork.Brands.Find(id) != null;
    }
}
