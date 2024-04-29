using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NotebookStoreMVC.Models;
using NotebookStore.Business;

namespace NotebookStoreMVC.Controllers;

public class CpuController : Controller
{
    private readonly CpuService service;
    private readonly IMapper mapper;

    public CpuController(CpuService service, IMapper mapper)
    {
        this.service = service;
        this.mapper = mapper;
    }

    // GET: CpuViewModel
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var cpus = await service.GetCpus();
        var mappedCpus = mapper.Map<IEnumerable<CpuViewModel>>(cpus);

        return View(mappedCpus);
    }

    // GET: CpuViewModel/Details/5
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var cpu = await service.GetCpu(id);

        if (cpu == null)
        {
            return NotFound();
        }

        return View(mapper.Map<CpuViewModel>(cpu));
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
    public async Task<IActionResult> Create([Bind("Id,Brand,Model")] CpuViewModel CpuViewModel)
    {
        if (ModelState.IsValid)
        {
            await service.CreateCpu(mapper.Map<CpuDto>(CpuViewModel));

            return RedirectToAction(nameof(Index));
        }

        return View(CpuViewModel);
    }

    // GET: CpuViewModel/Edit/5
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var cpu = await service.GetCpu(id);

        if (cpu == null)
        {
            return NotFound();
        }

        return View(mapper.Map<CpuViewModel>(cpu));
    }

    // POST: CpuViewModel/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Brand,Model")] CpuViewModel CpuViewModel)
    {
        if (id != CpuViewModel.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                await service.UpdateCpu(mapper.Map<CpuDto>(CpuViewModel));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }

            return RedirectToAction(nameof(Index));
        }

        return View(CpuViewModel);
    }


    // GET: CpuViewModel/Delete/5
    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var cpu = await service.GetCpu(id);

        if (cpu == null)
        {
            return NotFound();
        }

        return View(mapper.Map<CpuViewModel>(cpu));
    }

    // POST: CpuViewModel/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await service.DeleteCpu(id);

        return RedirectToAction(nameof(Index));
    }

    private async Task<bool> CpuExists(int id)
    {
        return await service.CpuExists(id);
    }
}
