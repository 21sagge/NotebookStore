using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NotebookStoreMVC.Models;
using NotebookStore.Business;

namespace NotebookStoreMVC.Controllers;

public class MemoryController : Controller
{
    private readonly MemoryService service;
    private readonly IMapper mapper;

    public MemoryController(MemoryService service, IMapper mapper)
    {
        this.service = service;
        this.mapper = mapper;
    }

    // GET: MemoryViewModel
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var memories = await service.GetMemories();
        var mappedMemories = mapper.Map<IEnumerable<MemoryViewModel>>(memories);

        return View(mappedMemories);
    }

    // GET: MemoryViewModel/Details/5
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var memory = await service.GetMemory(id);

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
    public async Task<IActionResult> Create([Bind("Id,Capacity,Speed")] MemoryViewModel MemoryViewModel)
    {
        if (ModelState.IsValid)
        {
            await service.CreateMemory(mapper.Map<MemoryDto>(MemoryViewModel));

            return RedirectToAction(nameof(Index));
        }

        return View(MemoryViewModel);
    }

    // GET: MemoryViewModel/Edit/5
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var memory = await service.GetMemory(id);

        if (memory == null)
        {
            return NotFound();
        }

        return View(mapper.Map<MemoryViewModel>(memory));
    }

    // POST: MemoryViewModel/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Capacity,Speed")] MemoryViewModel MemoryViewModel)
    {
        if (id != MemoryViewModel.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            await service.UpdateMemory(mapper.Map<MemoryDto>(MemoryViewModel));

            return RedirectToAction(nameof(Index));
        }

        return View(MemoryViewModel);
    }


    // GET: MemoryViewModel/Delete/5
    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var memory = await service.GetMemory(id);

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
        await service.DeleteMemory(id);

        return RedirectToAction(nameof(Index));
    }

    private async Task<bool> MemoryExists(int id)
    {
        return await service.MemoryExists(id);
    }
}
