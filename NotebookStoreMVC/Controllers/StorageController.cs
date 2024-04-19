using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotebookStoreMVC.Models;
using NotebookStore.DAL;
using NotebookStore.Entities;

namespace NotebookStoreMVC.Controllers;

public class StorageController : Controller
{
    private readonly IRepository<Storage> _storageRepository;
    private readonly IMapper mapper;

    public StorageController(IRepository<Storage> repository, IMapper mapper)
    {
        _storageRepository = repository;
        this.mapper = mapper;
    }

    // GET: StorageViewModel
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var storageViewModels = await _storageRepository.Read();

        return View(mapper.Map<IEnumerable<StorageViewModel>>(storageViewModels));
    }

    // GET: StorageViewModel/Details/5
    [HttpGet]
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || _storageRepository.Read() == null)
        {
            return NotFound();
        }

        var storageViewModel = await _storageRepository.Find(id);
        if (storageViewModel == null)
        {
            return NotFound();
        }

        return View(mapper.Map<StorageViewModel>(storageViewModel));
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
    public IActionResult Create([Bind("Id,Type,Capacity")] StorageViewModel StorageViewModel)
    {
        if (ModelState.IsValid)
        {
            _storageRepository.Create(mapper.Map<Storage>(StorageViewModel));
            return RedirectToAction(nameof(Index));
        }
        return View(mapper.Map<StorageViewModel>(StorageViewModel));
    }

    // GET: StorageViewModel/Edit/5
    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || _storageRepository.Read() == null)
        {
            return NotFound();
        }

        var storageViewModel = await _storageRepository.Find(id);
        if (storageViewModel == null)
        {
            return NotFound();
        }
        return View(mapper.Map<StorageViewModel>(storageViewModel));
    }

    // POST: StorageViewModel/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, [Bind("Id,Type,Capacity")] StorageViewModel storageViewModel)
    {
        if (id != storageViewModel.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _storageRepository.Update(mapper.Map<Storage>(storageViewModel));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StorageExists(storageViewModel.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(mapper.Map<StorageViewModel>(storageViewModel));
    }

    // GET: StorageViewModel/Delete/5
    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || _storageRepository.Read() == null)
        {
            return NotFound();
        }

        var storageViewModel = await _storageRepository.Find(id);
        if (storageViewModel == null)
        {
            return NotFound();
        }

        return View(mapper.Map<StorageViewModel>(storageViewModel));
    }

    // POST: StorageViewModel/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (_storageRepository.Read() == null)
        {
            return Problem("Entity set 'NotebookStoreContext.Storage'  is null.");
        }

        await _storageRepository.Delete(id);
        return RedirectToAction(nameof(Index));
    }

    private bool StorageExists(int id)
    {
        return _storageRepository.Find(id) != null;
    }
}
