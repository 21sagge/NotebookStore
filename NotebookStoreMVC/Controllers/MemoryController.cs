using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NotebookStoreMVC.Models;
using NotebookStore.DAL;
using NotebookStore.Entities;

namespace NotebookStoreMVC.Controllers;

public class MemoryController : Controller
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public MemoryController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    // GET: MemoryViewModel
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var memories = await unitOfWork.Memories.Read();

        return View(mapper.Map<IEnumerable<MemoryViewModel>>(memories));
    }

    // GET: MemoryViewModel/Details/5
    [HttpGet]
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || unitOfWork.Memories.Read() == null)
        {
            return NotFound();
        }

        var memory = await unitOfWork.Memories.Find(id);

        if (memory == null)
        {
            return NotFound();
        }

        return View(mapper.Map<MemoryViewModel>(memory));
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
        unitOfWork.BeginTransaction();

        try
        {
            unitOfWork.Memories.Create(mapper.Map<Memory>(MemoryViewModel));
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

    // GET: MemoryViewModel/Edit/5
    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || unitOfWork.Memories.Read() == null)
        {
            return NotFound();
        }

        var memory = await unitOfWork.Memories.Find(id);

        if (memory == null)
        {
            return NotFound();
        }

        return View(mapper.Map<MemoryViewModel>(memory));
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

        unitOfWork.BeginTransaction();

        try
        {
            unitOfWork.Memories.Update(mapper.Map<Memory>(MemoryViewModel));
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


    // GET: MemoryViewModel/Delete/5
    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || unitOfWork.Memories.Read() == null)
        {
            return NotFound();
        }

        var memory = await unitOfWork.Memories.Find(id);

        if (memory == null)
        {
            return NotFound();
        }

        return View(mapper.Map<MemoryViewModel>(memory));
    }

    // POST: MemoryViewModel/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (unitOfWork.Memories.Read() == null)
        {
            return Problem("Entity set 'NotebookStoreContext.Memories'  is null.");
        }

        unitOfWork.BeginTransaction();

        try
        {
            if (await unitOfWork.Memories.Find(id) == null)
            {
                return NotFound();
            }

            await unitOfWork.Memories.Delete(id);

            unitOfWork.CommitTransaction();

            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            unitOfWork.RollbackTransaction();
            return Problem(ex.Message);
        }
    }

    private bool MemoryExists(int id)
    {
        return unitOfWork.Memories.Find(id) != null;
    }
}
