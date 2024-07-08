using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NotebookStoreMVC.Models;
using NotebookStore.Business;
using Microsoft.AspNetCore.Authorization;

namespace NotebookStoreMVC.Controllers;

[Authorize(Roles = "Admin,Editor")]
public class CpuController : Controller
{
    private readonly IServices services;
    private readonly IMapper mapper;

    public CpuController(IServices services, IMapper mapper)
    {
        this.services = services;
        this.mapper = mapper;
    }

    // GET: CpuViewModel
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var cpus = await services.Cpus.GetAll();

        return View(mapper.Map<IEnumerable<CpuViewModel>>(cpus));
    }

    // GET: CpuViewModel/Details/5
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var cpu = await services.Cpus.Find(id);

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
            await services.Cpus.Create(mapper.Map<CpuDto>(CpuViewModel));

            return RedirectToAction(nameof(Index));
        }

        return View(CpuViewModel);
    }

    // GET: CpuViewModel/Edit/5
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var cpu = await services.Cpus.Find(id);

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
            var result = await services.Cpus.Update(mapper.Map<CpuDto>(CpuViewModel));

            if (result)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Unauthorized");
            }
        }

        return View(CpuViewModel);
    }


    // GET: CpuViewModel/Delete/5
    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var cpu = await services.Cpus.Find(id);

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
        await services.Cpus.Delete(id);

        return RedirectToAction(nameof(Index));
    }
}
