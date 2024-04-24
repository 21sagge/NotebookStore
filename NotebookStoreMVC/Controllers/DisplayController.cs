using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NotebookStoreMVC.Models;
using NotebookStore.Entities;
using NotebookStore.Business;

namespace NotebookStoreMVC.Controllers;

public class DisplayController : Controller
{
    private readonly DisplayService service;
    private readonly IMapper mapper;

    public DisplayController(DisplayService service, IMapper mapper)
    {
        this.service = service;
        this.mapper = mapper;
    }

    // GET: DisplayViewModel
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var displays = await service.GetDisplays();
        var mappedDisplays = mapper.Map<IEnumerable<DisplayViewModel>>(displays);

        return View(mappedDisplays);
    }

    // GET: DisplayViewModel/Details/5
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var display = await service.GetDisplay(id);

        if (display == null)
        {
            return NotFound();
        }

        return View(mapper.Map<DisplayViewModel>(display));
    }

    // GET: DisplayViewModel/Create
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    // POST: DisplayViewModel/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Brand,Model")] DisplayViewModel DisplayViewModel)
    {
        if (ModelState.IsValid)
        {
            await service.CreateDisplay(mapper.Map<DisplayDto>(DisplayViewModel));

            return RedirectToAction(nameof(Index));
        }

        return View(DisplayViewModel);
    }

    // GET: DisplayViewModel/Edit/5
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var display = await service.GetDisplay(id);

        if (display == null)
        {
            return NotFound();
        }

        return View(mapper.Map<DisplayViewModel>(display));
    }

    // POST: DisplayViewModel/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Brand,Model")] DisplayViewModel DisplayViewModel)
    {
        if (id != DisplayViewModel.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                await service.UpdateDisplay(mapper.Map<DisplayDto>(DisplayViewModel));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }

            return RedirectToAction(nameof(Index));
        }

        return View(DisplayViewModel);
    }


    // GET: DisplayViewModel/Delete/5
    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var display = await service.GetDisplay(id);

        if (display == null)
        {
            return NotFound();
        }

        return View(mapper.Map<DisplayViewModel>(display));
    }

    // POST: DisplayViewModel/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await service.DeleteDisplay(id);

        return RedirectToAction(nameof(Index));
    }

    private async Task<bool> DisplayExists(int id)
    {
        return await service.DisplayExists(id);
    }
}
