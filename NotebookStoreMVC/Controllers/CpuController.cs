using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotebookStoreMVC.Models;
using NotebookStore.DAL;
using NotebookStore.Entities;

namespace NotebookStoreMVC.Controllers;

public class CpuController : Controller
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public CpuController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    // GET: CpuViewModel
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        unitOfWork.BeginTransaction();

        try
        {
            var cpus = await unitOfWork.Cpus.Read();
            unitOfWork.CommitTransaction();
            return View(mapper.Map<IEnumerable<CpuViewModel>>(cpus));
        }
        catch (Exception ex)
        {
            unitOfWork.RollbackTransaction();
            return Problem(ex.Message);
        }
    }

    // GET: CpuViewModel/Details/5
    [HttpGet]
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || unitOfWork.Cpus.Read() == null)
        {
            return NotFound();
        }

        unitOfWork.BeginTransaction();

        try
        {
            var CpuViewModel = await unitOfWork.Cpus.Find(id);
            unitOfWork.CommitTransaction();

            if (CpuViewModel == null)
            {
                return NotFound();
            }

            return View(mapper.Map<CpuViewModel>(CpuViewModel));
        }
        catch (Exception ex)
        {
            unitOfWork.RollbackTransaction();
            return Problem(ex.Message);
        }
    }

    // GET: CpuViewModel/Create
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    // POST: CpuViewModel/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([Bind("Id,Brand,Model")] CpuViewModel CpuViewModel)
    {
        if (ModelState.IsValid)
        {
            unitOfWork.BeginTransaction();

            try
            {
                unitOfWork.Cpus.Create(mapper.Map<Cpu>(CpuViewModel));
                unitOfWork.CommitTransaction();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                unitOfWork.RollbackTransaction();
                return Problem(ex.Message);
            }
        }
        return View(CpuViewModel);
    }

    // GET: CpuViewModel/Edit/5
    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || unitOfWork.Cpus.Read() == null)
        {
            return NotFound();
        }

        unitOfWork.BeginTransaction();

        try
        {
            var CpuViewModel = await unitOfWork.Cpus.Find(id);
            unitOfWork.CommitTransaction();

            if (CpuViewModel == null)
            {
                return NotFound();
            }

            return View(mapper.Map<CpuViewModel>(CpuViewModel));
        }
        catch (Exception ex)
        {
            unitOfWork.RollbackTransaction();
            return Problem(ex.Message);
        }
    }

    // POST: CpuViewModel/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, [Bind("Id,Brand,Model")] CpuViewModel CpuViewModel)
    {
        if (id != CpuViewModel.Id)
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
                    unitOfWork.Cpus.Update(mapper.Map<Cpu>(CpuViewModel));
                    unitOfWork.CommitTransaction();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CpuExists(CpuViewModel.Id))
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
        return View(CpuViewModel);
    }

    // GET: CpuViewModel/Delete/5
    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || unitOfWork.Cpus.Read() == null)
        {
            return NotFound();
        }

        unitOfWork.BeginTransaction();

        try
        {
            var CpuViewModel = await unitOfWork.Cpus.Find(id);
            unitOfWork.CommitTransaction();

            if (CpuViewModel == null)
            {
                return NotFound();
            }

            return View(mapper.Map<CpuViewModel>(CpuViewModel));
        }
        catch (Exception ex)
        {
            unitOfWork.RollbackTransaction();
            return Problem(ex.Message);
        }
    }

    // POST: CpuViewModel/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (unitOfWork.Cpus.Read() == null)
        {
            return NotFound();
        }

        unitOfWork.BeginTransaction();

        try
        {
            await unitOfWork.Cpus.Delete(id);

            unitOfWork.CommitTransaction();

            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            unitOfWork.RollbackTransaction();
            return Problem(ex.Message);
        }
    }

    private bool CpuExists(int id)
    {
        return unitOfWork.Cpus.Find(id) != null;
    }
}
