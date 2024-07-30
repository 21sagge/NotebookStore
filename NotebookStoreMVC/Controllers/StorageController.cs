using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NotebookStoreMVC.Models;
using NotebookStore.Business;
using Microsoft.AspNetCore.Authorization;

namespace NotebookStoreMVC.Controllers;

[Authorize]
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
    [Authorize(Policy = Claims.ReadStorage)]
    public async Task<IActionResult> Index()
    {
        var storageDtos = await services.Storages.GetAll();

        return View(mapper.Map<IEnumerable<StorageViewModel>>(storageDtos));
    }

    // GET: StorageViewModel/Details/5
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var storageDto = await services.Storages.Find(id);

        if (storageDto == null)
        {
            return NotFound();
        }

        return View(mapper.Map<StorageViewModel>(storageDto));
    }

    // GET: StorageViewModel/Create
    [HttpGet]
    [Authorize(Policy = Claims.CreateStorage)]
    public IActionResult Create()
    {
        return View();
    }

    // POST: StorageViewModel/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = Claims.CreateStorage)]
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
    [Authorize(Policy = Claims.UpdateStorage)]
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
    [Authorize(Policy = Claims.UpdateStorage)]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Capacity,Type")] StorageViewModel StorageViewModel)
    {
        if (id != StorageViewModel.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            var result = await services.Storages.Update(mapper.Map<StorageDto>(StorageViewModel));

            if (result)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Unauthorized");
            }
        }

        return View(StorageViewModel);
    }

    // GET: StorageViewModel/Delete/5
    [HttpGet]
    [Authorize(Policy = Claims.DeleteStorage)]
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
    [Authorize(Policy = Claims.DeleteStorage)]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await services.Storages.Delete(id);

        return RedirectToAction(nameof(Index));
    }
}
