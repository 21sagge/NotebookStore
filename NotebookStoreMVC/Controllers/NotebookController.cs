using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NotebookStoreMVC.Models;
using NotebookStore.Business;
using Microsoft.AspNetCore.Authorization;

namespace NotebookStoreMVC.Controllers;

public class NotebookController : Controller
{
	private readonly IServices services;
	private readonly IMapper mapper;

	public NotebookController(IServices services, IMapper mapper)
	{
		this.services = services;
		this.mapper = mapper;
	}

	// GET: Notebook
	[HttpGet]
	[Authorize]
	public async Task<IActionResult> Index()
	{
		var notebookDtos = await services.Notebooks.GetAll();
		var notebookViewModels = mapper.Map<IEnumerable<NotebookViewModel>>(notebookDtos);

		foreach (var notebook in notebookDtos)
		{
			var notebookViewModel = notebookViewModels.FirstOrDefault(n => n.Id == notebook.Id);

			if (notebookViewModel != null)
			{
				notebookViewModel.CanUpdateAndDelete = notebook.CanUpdate && notebook.CanDelete;
			}
		}

		return View(notebookViewModels);
	}

	// GET: Notebook/Details/5
	[HttpGet]
	[Authorize(Roles = "Admin")]
	public async Task<IActionResult> Details(int id)
	{
		var notebook = await services.Notebooks.Find(id);

		if (notebook == null)
		{
			return NotFound();
		}

		var notebookViewModel = mapper.Map<NotebookViewModel>(notebook);

		notebookViewModel.CanUpdateAndDelete = notebook.CanUpdate && notebook.CanDelete;

		return View(notebookViewModel);
	}

	// GET: Notebook/Create
	[HttpGet]
	[Authorize(Roles = "Admin")]
	public async Task<IActionResult> Create()
	{
		ViewBag.Brands = mapper.Map<IEnumerable<BrandViewModel>>(await services.Brands.GetAll());
		ViewBag.Cpus = mapper.Map<IEnumerable<CpuViewModel>>(await services.Cpus.GetAll());
		ViewBag.Displays = mapper.Map<IEnumerable<DisplayViewModel>>(await services.Displays.GetAll());
		ViewBag.Memories = mapper.Map<IEnumerable<MemoryViewModel>>(await services.Memories.GetAll());
		ViewBag.Models = mapper.Map<IEnumerable<ModelViewModel>>(await services.Models.GetAll());
		ViewBag.Storages = mapper.Map<IEnumerable<StorageViewModel>>(await services.Storages.GetAll());

		return View();
	}

	// POST: Notebook/Create
	[HttpPost]
	[ValidateAntiForgeryToken]
	[Authorize(Roles = "Admin")]
	public async Task<IActionResult> Create([Bind("Id,Color,Price,BrandId,ModelId,CpuId,DisplayId,MemoryId,StorageId")] NotebookViewModel notebook)
	{
		ModelState.Remove("CreatedAt");

		if (ModelState.IsValid)
		{
			await services.Notebooks.Create(mapper.Map<NotebookDto>(notebook));

			return RedirectToAction(nameof(Index));
		}

		return View(notebook);
	}

	// GET: Notebook/Edit/5
	[HttpGet]
	[Authorize(Roles = "Admin")]
	public async Task<IActionResult> Edit(int id)
	{
		var notebook = await services.Notebooks.Find(id);

		if (notebook == null)
		{
			return NotFound();
		}

		ViewBag.Brands = mapper.Map<IEnumerable<BrandViewModel>>(await services.Brands.GetAll());
		ViewBag.Cpus = mapper.Map<IEnumerable<CpuViewModel>>(await services.Cpus.GetAll());
		ViewBag.Displays = mapper.Map<IEnumerable<DisplayViewModel>>(await services.Displays.GetAll());
		ViewBag.Memories = mapper.Map<IEnumerable<MemoryViewModel>>(await services.Memories.GetAll());
		ViewBag.Models = mapper.Map<IEnumerable<ModelViewModel>>(await services.Models.GetAll());
		ViewBag.Storages = mapper.Map<IEnumerable<StorageViewModel>>(await services.Storages.GetAll());

		return View(mapper.Map<NotebookViewModel>(notebook));
	}

	// POST: Notebook/Edit/5
	[HttpPost]
	[ValidateAntiForgeryToken]
	[Authorize(Roles = "Admin")]
	public async Task<IActionResult> Edit(int id, [Bind("Id,Color,Price,BrandId,ModelId,CpuId,DisplayId,MemoryId,StorageId,CreatedAt,CreatedBy")] NotebookViewModel notebook)
	{
		if (id != notebook.Id)
		{
			return NotFound();
		}

		if (ModelState.IsValid)
		{
			var result = await services.Notebooks.Update(mapper.Map<NotebookDto>(notebook));

			if (result)
			{
				return RedirectToAction(nameof(Index));
			}
			else
			{
				ModelState.AddModelError(string.Empty, "Update failed.");
			}
		}

		return View(notebook);
	}

	// GET: Notebook/Delete/5
	[HttpGet]
	[Authorize(Roles = "Admin")]
	public async Task<IActionResult> Delete(int id)
	{
		var notebook = await services.Notebooks.Find(id);

		if (notebook == null)
		{
			return NotFound();
		}

		return View(mapper.Map<NotebookViewModel>(notebook));
	}

	// POST: Notebook/Delete/5
	[HttpPost, ActionName("Delete")]
	[ValidateAntiForgeryToken]
	[Authorize(Roles = "Admin")]
	public async Task<IActionResult> DeleteConfirmed(int id)
	{
		await services.Notebooks.Delete(id);

		return RedirectToAction(nameof(Index));
	}
}
