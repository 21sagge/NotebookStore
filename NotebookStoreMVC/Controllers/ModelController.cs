using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NotebookStoreMVC.Models;
using NotebookStore.Business;
using Microsoft.AspNetCore.Authorization;

namespace NotebookStoreMVC.Controllers;

[Authorize(Roles = "Admin,Editor")]
public class ModelController : Controller
{
    private readonly IServices services;
    private readonly IMapper mapper;

    public ModelController(IServices services, IMapper mapper)
    {
        this.services = services;
        this.mapper = mapper;
    }

    // GET: ModelViewModel
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var models = await services.Models.GetAll();
        var mappedModels = mapper.Map<IEnumerable<ModelViewModel>>(models);

        return View(mappedModels);
    }

    // GET: ModelViewModel/Details/5
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var model = await services.Models.Find(id);

        if (model == null)
        {
            return NotFound();
        }

        return View(mapper.Map<ModelViewModel>(model));
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
    public async Task<IActionResult> Create([Bind("Id,Name")] ModelViewModel ModelViewModel)
    {
        ModelState.Remove("CreatedAt");

        if (ModelState.IsValid)
        {
            await services.Models.Create(mapper.Map<ModelDto>(ModelViewModel));

            return RedirectToAction(nameof(Index));
        }

        return View(ModelViewModel);
    }

    // GET: ModelViewModel/Edit/5
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var model = await services.Models.Find(id);

        if (model == null)
        {
            return NotFound();
        }

        return View(mapper.Map<ModelViewModel>(model));
    }

    // POST: ModelViewModel/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,CreatedAt,CreatedBy")] ModelViewModel ModelViewModel)
    {
        if (id != ModelViewModel.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            var result = await services.Models.Update(mapper.Map<ModelDto>(ModelViewModel));

            if (result)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Update failed. Please try again.");
            }
        }

        return View(ModelViewModel);
    }

    // GET: ModelViewModel/Delete/5
    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var model = await services.Models.Find(id);

        if (model == null)
        {
            return NotFound();
        }

        return View(mapper.Map<ModelViewModel>(model));
    }

    // POST: ModelViewModel/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await services.Models.Delete(id);

        return RedirectToAction(nameof(Index));
    }
}
