using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NotebookStoreMVC.Models;
using NotebookStore.DAL;
using NotebookStore.Entities;

namespace NotebookStoreMVC.Controllers;

public class StorageController : Controller
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public StorageController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    // GET: StorageViewModel
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var storages = await unitOfWork.Storages.Read();

        return View(mapper.Map<IEnumerable<StorageViewModel>>(storages));
    }

    // GET: StorageViewModel/Details/5
    [HttpGet]
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || unitOfWork.Storages.Read() == null)
        {
            return NotFound();
        }

        var storage = await unitOfWork.Storages.Find(id);

        if (storage == null)
        {
            return NotFound();
        }

        return View(mapper.Map<StorageViewModel>(storage));
    }

    // GET: StorageViewModel/Create
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    // POST: StorageViewModel/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([Bind("Id,Capacity,Type")] StorageViewModel StorageViewModel)
    {
        unitOfWork.BeginTransaction();

        try
        {
            unitOfWork.Storages.Create(mapper.Map<Storage>(StorageViewModel));
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

    // GET: StorageViewModel/Edit/5
    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || unitOfWork.Storages.Read() == null)
        {
            return NotFound();
        }

        var storage = await unitOfWork.Storages.Find(id);

        if (storage == null)
        {
            return NotFound();
        }

        return View(mapper.Map<StorageViewModel>(storage));
    }

    // POST: StorageViewModel/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, [Bind("Id,Capacity,Type")] StorageViewModel StorageViewModel)
    {
        if (id != StorageViewModel.Id)
        {
            return NotFound();
        }

        unitOfWork.BeginTransaction();

        try
        {
            unitOfWork.Storages.Update(mapper.Map<Storage>(StorageViewModel));
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


    // GET: StorageViewModel/Delete/5
    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || unitOfWork.Storages.Read() == null)
        {
            return NotFound();
        }

        var storage = await unitOfWork.Storages.Find(id);

        if (storage == null)
        {
            return NotFound();
        }

        return View(mapper.Map<StorageViewModel>(storage));
    }

    // POST: StorageViewModel/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (unitOfWork.Storages.Read() == null)
        {
            return Problem("Entity set 'NotebookStoreContext.Storages'  is null.");
        }

        unitOfWork.BeginTransaction();

        try
        {
            if (await unitOfWork.Storages.Find(id) == null)
            {
                return NotFound();
            }

            await unitOfWork.Storages.Delete(id);

            unitOfWork.CommitTransaction();

            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            unitOfWork.RollbackTransaction();
            return Problem(ex.Message);
        }
    }

    private bool StorageExists(int id)
    {
        return unitOfWork.Storages.Find(id) != null;
    }
}
