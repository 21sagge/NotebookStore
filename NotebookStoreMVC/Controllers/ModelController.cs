using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotebookStoreMVC.Models;
using NotebookStore.Repositories;
using NotebookStore.Entities;

namespace NotebookStoreMVC.Controllers;

public class ModelController : Controller
{
    private readonly IRepository<Model> _modelRepository;
    private readonly IMapper mapper;

    public ModelController(IRepository<Model> repository, IMapper mapper)
    {
        _modelRepository = repository;
        this.mapper = mapper;
    }

    // GET: ModelViewModel
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var modelViewModels = await _modelRepository.Read();
        return View(mapper.Map<IEnumerable<ModelViewModel>>(modelViewModels));
    }

    // GET: ModelViewModel/Details/5
    [HttpGet]
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || _modelRepository.Read() == null)
        {
            return NotFound();
        }

        var ModelViewModel = await _modelRepository.Find(id);
        if (ModelViewModel == null)
        {
            return NotFound();
        }

        return View(mapper.Map<ModelViewModel>(ModelViewModel));
    }

    // GET: ModelViewModel/Create
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    // POST: ModelViewModel/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([Bind("Id,Name")] ModelViewModel ModelViewModel)
    {
        if (ModelState.IsValid)
        {
            _modelRepository.Create(mapper.Map<Model>(ModelViewModel));
            return RedirectToAction(nameof(Index));
        }
        return View(mapper.Map<ModelViewModel>(ModelViewModel));
    }

    // GET: ModelViewModel/Edit/5
    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || _modelRepository.Read() == null)
        {
            return NotFound();
        }

        var ModelViewModel = await _modelRepository.Find(id);
        if (ModelViewModel == null)
        {
            return NotFound();
        }
        return View(mapper.Map<ModelViewModel>(ModelViewModel));
    }

    // POST: ModelViewModel/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, [Bind("Id,Name")] ModelViewModel ModelViewModel)
    {
        if (id != ModelViewModel.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _modelRepository.Update(mapper.Map<Model>(ModelViewModel));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ModelExists(ModelViewModel.Id))
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
        return View(mapper.Map<ModelViewModel>(ModelViewModel));
    }

    // GET: ModelViewModel/Delete/5
    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || _modelRepository.Read() == null)
        {
            return NotFound();
        }

        var ModelViewModel = await _modelRepository.Find(id);
        if (ModelViewModel == null)
        {
            return NotFound();
        }

        return View(mapper.Map<ModelViewModel>(ModelViewModel));
    }

    // POST: ModelViewModel/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (_modelRepository.Read() == null)
        {
            return Problem("Entity set 'NotebookStoreContext.Model'  is null.");
        }

        await _modelRepository.Delete(id);
        return RedirectToAction(nameof(Index));
    }

    private bool ModelExists(int id)
    {
        return _modelRepository.Find(id) != null;
    }
}
