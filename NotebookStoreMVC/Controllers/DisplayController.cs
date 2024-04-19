using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotebookStoreMVC.Models;
using NotebookStore.DAL;
using NotebookStore.Entities;

namespace NotebookStoreMVC.Controllers;

public class DisplayController : Controller
{
    private readonly IRepository<Display> _displayRepository;
    private readonly IMapper mapper;

    public DisplayController(IRepository<Display> repository, IMapper mapper)
    {
        _displayRepository = repository;
        this.mapper = mapper;
    }

    // GET: DisplayViewModel
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var displayViewModels = await _displayRepository.Read();
        return View(mapper.Map<IEnumerable<DisplayViewModel>>(displayViewModels));
    }

    // GET: DisplayViewModel/Details/5
    [HttpGet]
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || _displayRepository.Read() == null)
        {
            return NotFound();
        }

        var DisplayViewModel = await _displayRepository.Find(id);
        if (DisplayViewModel == null)
        {
            return NotFound();
        }

        return View(mapper.Map<DisplayViewModel>(DisplayViewModel));
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
    public IActionResult Create([Bind("Id,Size,ResolutionWidth,ResolutionHeight,PanelType")] DisplayViewModel DisplayViewModel)
    {
        if (ModelState.IsValid)
        {
            _displayRepository.Create(mapper.Map<Display>(DisplayViewModel));
            return RedirectToAction(nameof(Index));
        }
        return View(mapper.Map<DisplayViewModel>(DisplayViewModel));
    }

    // GET: DisplayViewModel/Edit/5
    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || _displayRepository.Read() == null)
        {
            return NotFound();
        }

        var DisplayViewModel = await _displayRepository.Find(id);
        if (DisplayViewModel == null)
        {
            return NotFound();
        }
        return View(mapper.Map<DisplayViewModel>(DisplayViewModel));
    }

    // POST: DisplayViewModel/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, [Bind("Id,Size,ResolutionWidth,ResolutionHeight,PanelType")] DisplayViewModel DisplayViewModel)
    {
        if (id != DisplayViewModel.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _displayRepository.Update(mapper.Map<Display>(DisplayViewModel));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DisplayExists(DisplayViewModel.Id))
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
        return View(mapper.Map<DisplayViewModel>(DisplayViewModel));
    }

    // GET: DisplayViewModel/Delete/5
    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || _displayRepository.Read() == null)
        {
            return NotFound();
        }

        var DisplayViewModel = await _displayRepository.Find(id);
        if (DisplayViewModel == null)
        {
            return NotFound();
        }

        return View(mapper.Map<DisplayViewModel>(DisplayViewModel));
    }

    // POST: DisplayViewModel/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (_displayRepository.Read() == null)
        {
            return Problem("Entity set 'NotebookStoreContext.Display'  is null.");
        }

        await _displayRepository.Delete(id);
        return RedirectToAction(nameof(Index));
    }

    private bool DisplayExists(int id)
    {
        return _displayRepository.Find(id) != null;
    }
}
