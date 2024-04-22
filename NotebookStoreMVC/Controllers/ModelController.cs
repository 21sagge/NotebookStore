using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotebookStoreMVC.Models;
using NotebookStore.DAL;
using NotebookStore.Entities;

namespace NotebookStoreMVC.Controllers;

public class ModelController : Controller
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public ModelController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    // GET: ModelViewModel
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        unitOfWork.BeginTransaction();

        try
        {
            var models = await unitOfWork.Models.Read();
            unitOfWork.CommitTransaction();
            return View(mapper.Map<IEnumerable<ModelViewModel>>(models));
        }
        catch (Exception ex)
        {
            unitOfWork.RollbackTransaction();
            return Problem(ex.Message);
        }
    }

    // GET: ModelViewModel/Details/5
    [HttpGet]
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || unitOfWork.Models.Read() == null)
        {
            return NotFound();
        }

        unitOfWork.BeginTransaction();

        try
        {
            var ModelViewModel = await unitOfWork.Models.Find(id);
            unitOfWork.CommitTransaction();

            if (ModelViewModel == null)
            {
                return NotFound();
            }

            return View(mapper.Map<ModelViewModel>(ModelViewModel));
        }
        catch (Exception ex)
        {
            unitOfWork.RollbackTransaction();
            return Problem(ex.Message);
        }
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
            unitOfWork.BeginTransaction();

            try
            {
                unitOfWork.Models.Create(mapper.Map<Model>(ModelViewModel));
                unitOfWork.CommitTransaction();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                unitOfWork.RollbackTransaction();
                return Problem(ex.Message);
            }
        }
        return View(ModelViewModel);
    }

    // GET: ModelViewModel/Edit/5
    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || unitOfWork.Models.Read() == null)
        {
            return NotFound();
        }

        unitOfWork.BeginTransaction();

        try
        {
            var ModelViewModel = await unitOfWork.Models.Find(id);
            unitOfWork.CommitTransaction();

            if (ModelViewModel == null)
            {
                return NotFound();
            }

            return View(mapper.Map<ModelViewModel>(ModelViewModel));
        }
        catch (Exception ex)
        {
            unitOfWork.RollbackTransaction();
            return Problem(ex.Message);
        }
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
            unitOfWork.BeginTransaction();

            try
            {
                try
                {
                    unitOfWork.Models.Update(mapper.Map<Model>(ModelViewModel));
                    unitOfWork.CommitTransaction();
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
            catch (Exception ex)
            {
                unitOfWork.RollbackTransaction();
                return Problem(ex.Message);
            }
        }
        return View(ModelViewModel);
    }

    // GET: ModelViewModel/Delete/5
    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || unitOfWork.Models.Read() == null)
        {
            return NotFound();
        }

        unitOfWork.BeginTransaction();

        try
        {
            var ModelViewModel = await unitOfWork.Models.Find(id);
            unitOfWork.CommitTransaction();

            if (ModelViewModel == null)
            {
                return NotFound();
            }

            return View(mapper.Map<ModelViewModel>(ModelViewModel));
        }
        catch (Exception ex)
        {
            unitOfWork.RollbackTransaction();
            return Problem(ex.Message);
        }
    }

    // POST: ModelViewModel/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (unitOfWork.Models.Read() == null)
        {
            return NotFound();
        }

        unitOfWork.BeginTransaction();

        try
        {
            await unitOfWork.Models.Delete(id);
            unitOfWork.CommitTransaction();
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            unitOfWork.RollbackTransaction();
            return Problem(ex.Message);
        }
    }

    private bool ModelExists(int id)
    {
        return unitOfWork.Models.Find(id) != null;
    }
}
