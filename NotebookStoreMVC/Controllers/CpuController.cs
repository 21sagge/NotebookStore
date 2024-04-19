using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotebookStoreMVC.Models;
using NotebookStore.DAL;
using NotebookStore.Entities;

namespace NotebookStoreMVC.Controllers;

public class CpuController : Controller
{
    private readonly IRepository<Cpu> _cpuRepository;
    private readonly IMapper mapper;

    public CpuController(IRepository<Cpu> repository, IMapper mapper)
    {
        _cpuRepository = repository;
        this.mapper = mapper;
    }

    // GET: CpuViewModel
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var cpuViewModels = await _cpuRepository.Read();
        return View(mapper.Map<IEnumerable<CpuViewModel>>(cpuViewModels));
    }

    // GET: CpuViewModel/Details/5
    [HttpGet]
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || _cpuRepository.Read() == null)
        {
            return NotFound();
        }

        var CpuViewModel = await _cpuRepository.Find(id);
        if (CpuViewModel == null)
        {
            return NotFound();
        }

        return View(mapper.Map<CpuViewModel>(CpuViewModel));
    }

    // GET: CpuViewModel/Create
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    // POST: CpuViewModel/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([Bind("Id,Brand,Model")] CpuViewModel CpuViewModel)
    {
        if (ModelState.IsValid)
        {
            _cpuRepository.Create(mapper.Map<Cpu>(CpuViewModel));
            return RedirectToAction(nameof(Index));
        }
        return View(mapper.Map<CpuViewModel>(CpuViewModel));
    }

    // GET: CpuViewModel/Edit/5
    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || _cpuRepository.Read() == null)
        {
            return NotFound();
        }

        var CpuViewModel = await _cpuRepository.Find(id);
        if (CpuViewModel == null)
        {
            return NotFound();
        }
        return View(mapper.Map<CpuViewModel>(CpuViewModel));
    }

    // POST: CpuViewModel/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, [Bind("Id,Brand,Model")] CpuViewModel CpuViewModel)
    {
        if (id != CpuViewModel.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _cpuRepository.Update(mapper.Map<Cpu>(CpuViewModel));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CpuExists(CpuViewModel.Id))
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
        return View(mapper.Map<CpuViewModel>(CpuViewModel));
    }

    // GET: CpuViewModel/Delete/5
    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || _cpuRepository.Read() == null)
        {
            return NotFound();
        }

        var CpuViewModel = await _cpuRepository.Find(id);
        if (CpuViewModel == null)
        {
            return NotFound();
        }

        return View(mapper.Map<CpuViewModel>(CpuViewModel));
    }

    // POST: CpuViewModel/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (_cpuRepository.Read() == null)
        {
            return Problem("Entity set 'NotebookStoreContext.Cpu'  is null.");
        }

        await _cpuRepository.Delete(id);
        return RedirectToAction(nameof(Index));
    }

    private bool CpuExists(int id)
    {
        return _cpuRepository.Find(id) != null;
    }
}
