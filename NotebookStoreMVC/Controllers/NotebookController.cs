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

		return View(notebookViewModels);
	}

	// GET: Notebook/Details/5
	[HttpGet]
	[Authorize]
	public async Task<IActionResult> Details(int id)
	{
		var notebook = await services.Notebooks.Find(id);

		if (notebook == null)
		{
			return NotFound();
		}

		var notebookViewModel = mapper.Map<NotebookViewModel>(notebook);

		return View(notebookViewModel);
	}

	// GET: Notebook/Create
	[HttpGet]
	[Authorize(Roles = "Admin, Editor")]
	public async Task<IActionResult> Create()
	{
		var brandDtos = await services.Brands.GetAll();
		var cpuDtos = await services.Cpus.GetAll();
		var displayDtos = await services.Displays.GetAll();
		var memoryDtos = await services.Memories.GetAll();
		var modelDtos = await services.Models.GetAll();
		var storageDtos = await services.Storages.GetAll();

		ViewBag.Brands = mapper.Map<IEnumerable<BrandViewModel>>(brandDtos);
		ViewBag.Cpus = mapper.Map<IEnumerable<CpuViewModel>>(cpuDtos);
		ViewBag.Displays = mapper.Map<IEnumerable<DisplayViewModel>>(displayDtos);
		ViewBag.Memories = mapper.Map<IEnumerable<MemoryViewModel>>(memoryDtos);
		ViewBag.Models = mapper.Map<IEnumerable<ModelViewModel>>(modelDtos);
		ViewBag.Storages = mapper.Map<IEnumerable<StorageViewModel>>(storageDtos);

		return View();
	}

	// POST: Notebook/Create
	[HttpPost]
	[ValidateAntiForgeryToken]
	[Authorize(Roles = "Admin, Editor")]
	public async Task<IActionResult> Create([Bind("Id,Color,Price,BrandId,ModelId,CpuId,DisplayId,MemoryId,StorageId")] NotebookViewModel notebook)
	{
		if (ModelState.IsValid)
		{
			await services.Notebooks.Create(mapper.Map<NotebookDto>(notebook));

			return RedirectToAction(nameof(Index));
		}

		return View(notebook);
	}

	// GET: Notebook/Edit/5
	[HttpGet]
	[Authorize(Roles = "Admin, Editor")]
	public async Task<IActionResult> Edit(int id)
	{
		var notebook = await services.Notebooks.Find(id);

		if (notebook == null)
		{
			return NotFound();
		}

		var brandDtos = await services.Brands.GetAll();
		var cpuDtos = await services.Cpus.GetAll();
		var displayDtos = await services.Displays.GetAll();
		var memoryDtos = await services.Memories.GetAll();
		var modelDtos = await services.Models.GetAll();
		var storageDtos = await services.Storages.GetAll();

		ViewBag.Brands = mapper.Map<IEnumerable<BrandViewModel>>(brandDtos);
		ViewBag.Cpus = mapper.Map<IEnumerable<CpuViewModel>>(cpuDtos);
		ViewBag.Displays = mapper.Map<IEnumerable<DisplayViewModel>>(displayDtos);
		ViewBag.Memories = mapper.Map<IEnumerable<MemoryViewModel>>(memoryDtos);
		ViewBag.Models = mapper.Map<IEnumerable<ModelViewModel>>(modelDtos);
		ViewBag.Storages = mapper.Map<IEnumerable<StorageViewModel>>(storageDtos);

		return View(mapper.Map<NotebookViewModel>(notebook));
	}

	// POST: Notebook/Edit/5
	[HttpPost]
	[ValidateAntiForgeryToken]
	[Authorize(Roles = "Admin, Editor")]
	public async Task<IActionResult> Edit(int id, [Bind("Id,Color,Price,BrandId,ModelId,CpuId,DisplayId,MemoryId,StorageId")] NotebookViewModel notebook)
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
	[Authorize(Roles = "Admin,Editor")]
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
	[Authorize(Roles = "Admin, Editor")]
	public async Task<IActionResult> DeleteConfirmed(int id)
	{
		await services.Notebooks.Delete(id);

		return RedirectToAction(nameof(Index));
	}
}
