using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotebookStoreMVC.Models;
using NotebookStore.DAL;
using NotebookStore.Entities;

namespace NotebookStoreMVC.Controllers;

public class MemoryController : Controller
{
    private readonly IRepository<Memory> _memoryRepository;
    private readonly IMapper mapper;

    public MemoryController(IRepository<Memory> repository, IMapper mapper)
    {
        _memoryRepository = repository;
        this.mapper = mapper;
    }

    // GET: MemoryViewModel
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var memoryViewModels = await _memoryRepository.Read();
        return View(mapper.Map<IEnumerable<MemoryViewModel>>(memoryViewModels));
    }

    // GET: MemoryViewModel/Details/5
    [HttpGet]
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || _memoryRepository.Read() == null)
        {
            return NotFound();
        }

        var MemoryViewModel = await _memoryRepository.Find(id);
        if (MemoryViewModel == null)
        {
            return NotFound();
        }

        return View(mapper.Map<MemoryViewModel>(MemoryViewModel));
    }

    // GET: MemoryViewModel/Create
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    // POST: MemoryViewModel/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([Bind("Id,Capacity,Speed")] MemoryViewModel MemoryViewModel)
    {
        if (ModelState.IsValid)
        {
            _memoryRepository.Create(mapper.Map<Memory>(MemoryViewModel));
            return RedirectToAction(nameof(Index));
        }
        return View(mapper.Map<MemoryViewModel>(MemoryViewModel));
    }

    // GET: MemoryViewModel/Edit/5
    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || _memoryRepository.Read() == null)
        {
            return NotFound();
        }

        var MemoryViewModel = await _memoryRepository.Find(id);
        if (MemoryViewModel == null)
        {
            return NotFound();
        }
        return View(mapper.Map<MemoryViewModel>(MemoryViewModel));
    }

    // POST: MemoryViewModel/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, [Bind("Id,Capacity,Speed")] MemoryViewModel MemoryViewModel)
    {
        if (id != MemoryViewModel.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _memoryRepository.Update(mapper.Map<Memory>(MemoryViewModel));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MemoryExists(MemoryViewModel.Id))
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
        return View(mapper.Map<MemoryViewModel>(MemoryViewModel));
    }

    // GET: MemoryViewModel/Delete/5
    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || _memoryRepository.Read() == null)
        {
            return NotFound();
        }

        var MemoryViewModel = await _memoryRepository.Find(id);
        if (MemoryViewModel == null)
        {
            return NotFound();
        }

        return View(mapper.Map<MemoryViewModel>(MemoryViewModel));
    }

    // POST: MemoryViewModel/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (_memoryRepository.Read() == null)
        {
            return Problem("Entity set 'NotebookStoreContext.Memory'  is null.");
        }

        await _memoryRepository.Delete(id);
        return RedirectToAction(nameof(Index));
    }

    private bool MemoryExists(int id)
    {
        return _memoryRepository.Find(id) != null;
    }
}
