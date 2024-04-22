using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotebookStoreMVC.Models;
using NotebookStore.DAL;
using NotebookStore.Entities;

namespace NotebookStoreMVC.Controllers;

public class DisplayController : Controller
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public DisplayController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    // GET: DisplayViewModel
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        unitOfWork.BeginTransaction();

        try
        {
            var displays = await unitOfWork.Displays.Read();
            unitOfWork.CommitTransaction();
            return View(mapper.Map<IEnumerable<DisplayViewModel>>(displays));
        }
        catch (Exception ex)
        {
            unitOfWork.RollbackTransaction();
            return Problem(ex.Message);
        }
    }

    // GET: DisplayViewModel/Details/5
    [HttpGet]
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || unitOfWork.Displays.Read() == null)
        {
            return NotFound();
        }

        unitOfWork.BeginTransaction();

        try
        {
            var DisplayViewModel = await unitOfWork.Displays.Find(id);
            unitOfWork.CommitTransaction();

            if (DisplayViewModel == null)
            {
                return NotFound();
            }

            return View(mapper.Map<DisplayViewModel>(DisplayViewModel));
        }
        catch (Exception ex)
        {
            unitOfWork.RollbackTransaction();
            return Problem(ex.Message);
        }
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
            unitOfWork.BeginTransaction();

            try
            {
                unitOfWork.Displays.Create(mapper.Map<Display>(DisplayViewModel));
                unitOfWork.CommitTransaction();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                unitOfWork.RollbackTransaction();
                return Problem(ex.Message);
            }
        }
        return View(DisplayViewModel);
    }

    // GET: DisplayViewModel/Edit/5
    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || unitOfWork.Displays.Read() == null)
        {
            return NotFound();
        }

        unitOfWork.BeginTransaction();

        try
        {
            var DisplayViewModel = await unitOfWork.Displays.Find(id);
            unitOfWork.CommitTransaction();

            if (DisplayViewModel == null)
            {
                return NotFound();
            }

            return View(mapper.Map<DisplayViewModel>(DisplayViewModel));
        }
        catch (Exception ex)
        {
            unitOfWork.RollbackTransaction();
            return Problem(ex.Message);
        }
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
            unitOfWork.BeginTransaction();

            try
            {
                try
                {
                    unitOfWork.Displays.Update(mapper.Map<Display>(DisplayViewModel));
                    unitOfWork.CommitTransaction();
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
            catch (Exception ex)
            {
                unitOfWork.RollbackTransaction();
                return Problem(ex.Message);
            }
        }
        return View(DisplayViewModel);
    }

    // GET: DisplayViewModel/Delete/5
    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || unitOfWork.Displays.Read() == null)
        {
            return NotFound();
        }

        unitOfWork.BeginTransaction();

        try
        {
            var DisplayViewModel = await unitOfWork.Displays.Find(id);
            unitOfWork.CommitTransaction();

            if (DisplayViewModel == null)
            {
                return NotFound();
            }

            return View(mapper.Map<DisplayViewModel>(DisplayViewModel));
        }
        catch (Exception ex)
        {
            unitOfWork.RollbackTransaction();
            return Problem(ex.Message);
        }
    }

    // POST: DisplayViewModel/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (unitOfWork.Displays.Read() == null)
        {
            return NotFound();
        }

        unitOfWork.BeginTransaction();

        try
        {
            await unitOfWork.Displays.Delete(id);
            unitOfWork.CommitTransaction();
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            unitOfWork.RollbackTransaction();
            return Problem(ex.Message);
        }
    }

    private bool DisplayExists(int id)
    {
        return unitOfWork.Displays.Find(id) != null;
    }
}
