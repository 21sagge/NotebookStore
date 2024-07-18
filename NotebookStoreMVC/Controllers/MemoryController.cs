using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NotebookStoreMVC.Models;
using NotebookStore.Business;
using Microsoft.AspNetCore.Authorization;

namespace NotebookStoreMVC.Controllers;

[Authorize(Roles = "Admin,Editor")]
public class MemoryController : Controller
{
    private readonly IServices services;
    private readonly IMapper mapper;

    public MemoryController(IServices services, IMapper mapper)
    {
        this.services = services;
        this.mapper = mapper;
    }

    // GET: MemoryViewModel
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var memories = await services.Memories.GetAll();

        return View(mapper.Map<IEnumerable<MemoryViewModel>>(memories));
    }

    // GET: MemoryViewModel/Details/5
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var memory = await services.Memories.Find(id);

        if (memory == null)
        {
            return NotFound();
        }

        return View(mapper.Map<MemoryViewModel>(memory));
    }

    // GET: MemoryViewModel/Create
    [HttpGet]
    [Authorize(Policy = "Add Memory")]
    public IActionResult Create()
    {
        return View();
    }

    // POST: MemoryViewModel/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = "Add Memory")]
    public async Task<IActionResult> Create([Bind("Id,Capacity,Speed")] MemoryViewModel MemoryViewModel)
    {
        if (ModelState.IsValid)
        {
            await services.Memories.Create(mapper.Map<MemoryDto>(MemoryViewModel));

            return RedirectToAction(nameof(Index));
        }

        return View(MemoryViewModel);
    }

    // GET: MemoryViewModel/Edit/5
    [HttpGet]
    [Authorize(Policy = "Edit Memory")]
    public async Task<IActionResult> Edit(int id)
    {
        var memory = await services.Memories.Find(id);

        if (memory == null)
        {
            return NotFound();
        }

        return View(mapper.Map<MemoryViewModel>(memory));
    }

    // POST: MemoryViewModel/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = "Edit Memory")]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Capacity,Speed")] MemoryViewModel MemoryViewModel)
    {
        if (id != MemoryViewModel.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            var result = await services.Memories.Update(mapper.Map<MemoryDto>(MemoryViewModel));

            if (result)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError(string.Empty, "You are not allowed to update this memory");
            }
        }

        return View(MemoryViewModel);
    }


    // GET: MemoryViewModel/Delete/5
    [HttpGet]
    [Authorize(Policy = "Delete Memory")]
    public async Task<IActionResult> Delete(int id)
    {
        var memory = await services.Memories.Find(id);

        if (memory == null)
        {
            return NotFound();
        }

        return View(mapper.Map<MemoryViewModel>(memory));
    }

    // POST: MemoryViewModel/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = "Delete Memory")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await services.Memories.Delete(id);

        return RedirectToAction(nameof(Index));
    }
}
