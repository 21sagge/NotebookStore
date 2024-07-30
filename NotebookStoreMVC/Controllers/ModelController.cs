using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NotebookStoreMVC.Models;
using NotebookStore.Business;
using Microsoft.AspNetCore.Authorization;

namespace NotebookStoreMVC.Controllers;

[Authorize]
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
    [Authorize(Policy = Claims.ReadModel)]
    public async Task<IActionResult> Index()
    {
        var models = await services.Models.GetAll();

        return View(mapper.Map<IEnumerable<ModelViewModel>>(models));
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
    [Authorize(Policy = Claims.CreateModel)]
    public IActionResult Create()
    {
        return View();
    }

    // POST: ModelViewModel/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = Claims.CreateModel)]
    public async Task<IActionResult> Create([Bind("Id,Name")] ModelViewModel ModelViewModel)
    {
        if (ModelState.IsValid)
        {
            await services.Models.Create(mapper.Map<ModelDto>(ModelViewModel));

            return RedirectToAction(nameof(Index));
        }

        return View(ModelViewModel);
    }

    // GET: ModelViewModel/Edit/5
    [HttpGet]
    [Authorize(Policy = Claims.UpdateModel)]
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
    [Authorize(Policy = Claims.UpdateModel)]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] ModelViewModel ModelViewModel)
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
                ModelState.AddModelError(string.Empty, "You can't update this model");
            }
        }

        return View(ModelViewModel);
    }

    // GET: ModelViewModel/Delete/5
    [HttpGet]
    [Authorize(Policy = Claims.DeleteModel)]
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
    [Authorize(Policy = Claims.DeleteModel)]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await services.Models.Delete(id);

        return RedirectToAction(nameof(Index));
    }
}
