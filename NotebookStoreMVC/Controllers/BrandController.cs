using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotebookStoreMVC.Models;
using NotebookStore.Repositories;
using NotebookStore.Entities;

namespace NotebookStoreMVC.Controllers;

public class BrandController : Controller
{
    private readonly IRepository<Brand> _brandRepository;
    private readonly IMapper mapper;

    public BrandController(IRepository<Brand> brandRepository, IMapper mapper)
    {
        _brandRepository = brandRepository;
        this.mapper = mapper;
    }

    // GET: BrandViewModel
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var brands = await _brandRepository.Read();
        return View(mapper.Map<IEnumerable<BrandViewModel>>(brands));
    }

    // GET: BrandViewModel/Details/5
    [HttpGet]
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || _brandRepository.Read() == null)
        {
            return NotFound();
        }

        var BrandViewModel = await _brandRepository.Find(id);
        if (BrandViewModel == null)
        {
            return NotFound();
        }

        return View(mapper.Map<BrandViewModel>(BrandViewModel));
    }

    // GET: BrandViewModel/Create
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    // POST: BrandViewModel/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([Bind("Id,Name")] BrandViewModel BrandViewModel)
    {
        if (ModelState.IsValid)
        {
            _brandRepository.Create(mapper.Map<Brand>(BrandViewModel));
            return RedirectToAction(nameof(Index));
        }
        return View(mapper.Map<BrandViewModel>(BrandViewModel));
    }

    // GET: BrandViewModel/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || _brandRepository.Read() == null)
        {
            return NotFound();
        }

        var BrandViewModel = await _brandRepository.Find(id);
        if (BrandViewModel == null)
        {
            return NotFound();
        }
        return View(mapper.Map<BrandViewModel>(BrandViewModel));
    }

    // POST: BrandViewModel/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, [Bind("Id,Name")] BrandViewModel BrandViewModel)
    {
        if (id != BrandViewModel.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _brandRepository.Update(mapper.Map<Brand>(BrandViewModel));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BrandExists(BrandViewModel.Id))
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
        return View(mapper.Map<BrandViewModel>(BrandViewModel));
    }

    // GET: BrandViewModel/Delete/5
    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || _brandRepository.Read() == null)
        {
            return NotFound();
        }

        var BrandViewModel = await _brandRepository.Find(id);
        if (BrandViewModel == null)
        {
            return NotFound();
        }

        return View(mapper.Map<BrandViewModel>(BrandViewModel));
    }

    // POST: BrandViewModel/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (_brandRepository.Read() == null)
        {
            return Problem("Entity set 'NotebookStoreContext.Brands'  is null.");
        }

        await _brandRepository.Delete(id);
        return RedirectToAction(nameof(Index));
    }

    private bool BrandExists(int id)
    {
        return _brandRepository.Find(id) != null;
    }
}
