using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NotebookStoreMVC.Models;
using NotebookStore.Business;

namespace NotebookStoreMVC.Controllers;

public class NotebookController : Controller
{
	private readonly NotebookService service;
	private readonly IMapper mapper;

	public NotebookController(NotebookService service, IMapper mapper)
	{
		this.service = service;
		this.mapper = mapper;
	}

	// GET: Notebook
	[HttpGet]
	public async Task<IActionResult> Index()
	{
		var notebooks = await service.GetNotebooks();
		var mappedNotebooks = mapper.Map<IEnumerable<NotebookViewModel>>(notebooks);

		return View(mappedNotebooks);
	}

	// GET: Notebook/Details/5
	[HttpGet]
	public async Task<IActionResult> Details(int id)
	{
		var notebook = await service.GetNotebook(id);

		if (notebook == null)
		{
			return NotFound();
		}

		return View(mapper.Map<NotebookViewModel>(notebook));
	}

	// GET: Notebook/Create
	[HttpGet]
	public async Task<IActionResult> Create()
	{
		ViewBag.Brands = mapper.Map<IEnumerable<BrandViewModel>>(await service.GetBrands());
		ViewBag.Cpus = mapper.Map<IEnumerable<CpuViewModel>>(await service.GetCpus());
		ViewBag.Displays = mapper.Map<IEnumerable<DisplayViewModel>>(await service.GetDisplays());
		ViewBag.Memories = mapper.Map<IEnumerable<MemoryViewModel>>(await service.GetMemories());
		ViewBag.Models = mapper.Map<IEnumerable<ModelViewModel>>(await service.GetModels());
		ViewBag.Storages = mapper.Map<IEnumerable<StorageViewModel>>(await service.GetStorages());

		return View();
	}

	// POST: Notebook/Create
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Create([Bind("Id,Color,Price,BrandId,ModelId,CpuId,DisplayId,MemoryId,StorageId")] NotebookViewModel notebook)
	{
		if (ModelState.IsValid)
		{
			await service.CreateNotebook(mapper.Map<NotebookDto>(notebook));

			return RedirectToAction(nameof(Index));
		}

		return View(notebook);
	}

	// GET: Notebook/Edit/5
	[HttpGet]
	public async Task<IActionResult> Edit(int id)
	{
		var notebook = await service.GetNotebook(id);

		if (notebook == null)
		{
			return NotFound();
		}

		ViewBag.Brands = mapper.Map<IEnumerable<BrandViewModel>>(await service.GetBrands());
		ViewBag.Cpus = mapper.Map<IEnumerable<CpuViewModel>>(await service.GetCpus());
		ViewBag.Displays = mapper.Map<IEnumerable<DisplayViewModel>>(await service.GetDisplays());
		ViewBag.Memories = mapper.Map<IEnumerable<MemoryViewModel>>(await service.GetMemories());
		ViewBag.Models = mapper.Map<IEnumerable<ModelViewModel>>(await service.GetModels());
		ViewBag.Storages = mapper.Map<IEnumerable<StorageViewModel>>(await service.GetStorages());

		return View(mapper.Map<NotebookViewModel>(notebook));
	}

	// POST: Notebook/Edit/5
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Edit(int id, [Bind("Id,Color,Price,BrandId,ModelId,CpuId,DisplayId,MemoryId,StorageId")] NotebookViewModel notebook)
	{
		if (id != notebook.Id)
		{
			return NotFound();
		}

		if (ModelState.IsValid)
		{
			await service.UpdateNotebook(mapper.Map<NotebookDto>(notebook));

			return RedirectToAction(nameof(Index));
		}

		return View(notebook);
	}

	// GET: Notebook/Delete/5
	[HttpGet]
	public async Task<IActionResult> Delete(int id)
	{
		var notebook = await service.GetNotebook(id);

		if (notebook == null)
		{
			return NotFound();
		}

		return View(mapper.Map<NotebookViewModel>(notebook));
	}

	// POST: Notebook/Delete/5
	[HttpPost, ActionName("Delete")]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> DeleteConfirmed(int id)
	{
		await service.DeleteNotebook(id);

		return RedirectToAction(nameof(Index));
	}

	private Task<bool> NotebookExists(int id)
	{
		return service.NotebookExists(id);
	}
}
