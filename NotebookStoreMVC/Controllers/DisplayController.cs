using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NotebookStoreMVC.Models;
using NotebookStore.Business;
using Microsoft.AspNetCore.Authorization;

namespace NotebookStoreMVC.Controllers;

[Authorize]
public class DisplayController : Controller
{
    private readonly IServices services;
    private readonly IMapper mapper;

    public DisplayController(IServices services, IMapper mapper)
    {
        this.services = services;
        this.mapper = mapper;
    }

    // GET: DisplayViewModel
    [HttpGet]
    [Authorize(Policy = Claims.ReadDisplay)]
    public async Task<IActionResult> Index()
    {
        var displays = await services.Displays.GetAll();

        return View(mapper.Map<IEnumerable<DisplayViewModel>>(displays));
    }

    // GET: DisplayViewModel/Details/5
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var display = await services.Displays.Find(id);

        if (display == null)
        {
            return NotFound();
        }

        return View(mapper.Map<DisplayViewModel>(display));
    }

    // GET: DisplayViewModel/Create
    [HttpGet]
    [Authorize(Policy = Claims.CreateDisplay)]
    public IActionResult Create()
    {
        return View();
    }

    // POST: DisplayViewModel/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = Claims.CreateDisplay)]
    public async Task<IActionResult> Create([Bind("Id, Size, ResolutionWidth, ResolutionHeight, PanelType")] DisplayViewModel DisplayViewModel)
    {
        if (ModelState.IsValid)
        {
            await services.Displays.Create(mapper.Map<DisplayDto>(DisplayViewModel));

            return RedirectToAction(nameof(Index));
        }

        return View(DisplayViewModel);
    }

    // GET: DisplayViewModel/Edit/5
    [HttpGet]
    [Authorize(Policy = Claims.UpdateDisplay)]
    public async Task<IActionResult> Edit(int id)
    {
        var display = await services.Displays.Find(id);

        if (display == null)
        {
            return NotFound();
        }

        return View(mapper.Map<DisplayViewModel>(display));
    }

    // POST: DisplayViewModel/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = Claims.UpdateDisplay)]
    public async Task<IActionResult> Edit(int id, [Bind("Id, Size, ResolutionWidth, ResolutionHeight, PanelType")] DisplayViewModel DisplayViewModel)
    {
        if (id != DisplayViewModel.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            var result = await services.Displays.Update(mapper.Map<DisplayDto>(DisplayViewModel));

            if (result)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Update failed.");
            }
        }

        return View(DisplayViewModel);
    }


    // GET: DisplayViewModel/Delete/5
    [HttpGet]
    [Authorize(Policy = "Delete Display")]
    public async Task<IActionResult> Delete(int id)
    {
        var display = await services.Displays.Find(id);

        if (display == null)
        {
            return NotFound();
        }

        return View(mapper.Map<DisplayViewModel>(display));
    }

    // POST: DisplayViewModel/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = "Delete Display")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await services.Displays.Delete(id);

        return RedirectToAction(nameof(Index));
    }
}
