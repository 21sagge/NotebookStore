using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NotebookStoreMVC.Models;
using NotebookStore.Business;

namespace NotebookStoreMVC.Controllers;

public class StorageController : Controller
{
    private readonly IServices services;
    private readonly IMapper mapper;

    public StorageController(IServices services, IMapper mapper)
    {
        this.services = services;
        this.mapper = mapper;
    }

    // GET: StorageViewModel
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var storages = await services.Storages.GetAll();
        var mappedStorages = mapper.Map<IEnumerable<StorageViewModel>>(storages);

        return View(mappedStorages);
    }

    // GET: StorageViewModel/Details/5
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var storage = await services.Storages.Find(id);

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
    public async Task<IActionResult> Create([Bind("Id,Capacity,Type")] StorageViewModel StorageViewModel)
    {
        if (ModelState.IsValid)
        {
            await services.Storages.Create(mapper.Map<StorageDto>(StorageViewModel));

            return RedirectToAction(nameof(Index));
        }
        return View(StorageViewModel);
    }

    // GET: StorageViewModel/Edit/5
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var storage = await services.Storages.Find(id);

        if (storage == null)
        {
            return NotFound();
        }

        return View(mapper.Map<StorageViewModel>(storage));
    }

    // POST: StorageViewModel/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Capacity,Type")] StorageViewModel StorageViewModel)
    {
        if (id != StorageViewModel.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            await services.Storages.Update(mapper.Map<StorageDto>(StorageViewModel));

            return RedirectToAction(nameof(Index));
        }
        return View(StorageViewModel);
    }


    // GET: StorageViewModel/Delete/5
    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var storage = await services.Storages.Find(id);

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
        await services.Storages.Delete(id);

        return RedirectToAction(nameof(Index));
    }
}
