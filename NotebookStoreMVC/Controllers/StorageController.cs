using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NotebookStoreMVC.Models;
using NotebookStore.Business;

namespace NotebookStoreMVC.Controllers;

public class StorageController : Controller
{
    private readonly StorageService service;
    private readonly IMapper mapper;

    public StorageController(StorageService service, IMapper mapper)
    {
        this.service = service;
        this.mapper = mapper;
    }

    // GET: StorageViewModel
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var storages = await service.GetStorages();
        var mappedStorages = mapper.Map<IEnumerable<StorageViewModel>>(storages);

        return View(mappedStorages);
    }

    // GET: StorageViewModel/Details/5
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var storage = await service.GetStorage(id);

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
    public async Task<IActionResult> Create([Bind("Id,Brand,Model")] StorageViewModel StorageViewModel)
    {
        if (ModelState.IsValid)
        {
            await service.CreateStorage(mapper.Map<StorageDto>(StorageViewModel));

            return RedirectToAction(nameof(Index));
        }
        return View(StorageViewModel);
    }

    // GET: StorageViewModel/Edit/5
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var storage = await service.GetStorage(id);

        if (storage == null)
        {
            return NotFound();
        }

        return View(mapper.Map<StorageViewModel>(storage));
    }

    // POST: StorageViewModel/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Brand,Model")] StorageViewModel StorageViewModel)
    {
        if (id != StorageViewModel.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            await service.UpdateStorage(mapper.Map<StorageDto>(StorageViewModel));

            return RedirectToAction(nameof(Index));
        }
        return View(StorageViewModel);
    }


    // GET: StorageViewModel/Delete/5
    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var storage = await service.GetStorage(id);

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
        await service.DeleteStorage(id);

        return RedirectToAction(nameof(Index));
    }

    private async Task<bool> StorageExists(int id)
    {
        return await service.StorageExists(id);
    }
}
