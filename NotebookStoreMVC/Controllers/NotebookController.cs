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
	[Authorize]
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
	[Authorize(Roles = "Admin, Editor")]
	public async Task<IActionResult> Create()
	{
		var brandDtos = await services.Brands.GetAll();
		var brandViewModels = mapper.Map<IEnumerable<BrandViewModel>>(brandDtos);

		foreach (var brandDto in brandDtos)
		{
			var brandViewModel = brandViewModels.FirstOrDefault(b => b.Id == brandDto.Id);

			if (brandViewModel != null)
			{
				brandViewModel.CanUpdateAndDelete = brandDto.CanUpdate && brandDto.CanDelete;
			}
		}

		var cpuDtos = await services.Cpus.GetAll();
		var cpuViewModels = mapper.Map<IEnumerable<CpuViewModel>>(cpuDtos);

		foreach (var cpuDto in cpuDtos)
		{
			var cpuViewModel = cpuViewModels.FirstOrDefault(c => c.Id == cpuDto.Id);

			if (cpuViewModel != null)
			{
				cpuViewModel.CanUpdateAndDelete = cpuDto.CanUpdate && cpuDto.CanDelete;
			}
		}

		var displayDtos = await services.Displays.GetAll();
		var displayViewModels = mapper.Map<IEnumerable<DisplayViewModel>>(displayDtos);

		foreach (var displayDto in displayDtos)
		{
			var displayViewModel = displayViewModels.FirstOrDefault(d => d.Id == displayDto.Id);

			if (displayViewModel != null)
			{
				displayViewModel.CanUpdateAndDelete = displayDto.CanUpdate && displayDto.CanDelete;
			}
		}

		var memoryDtos = await services.Memories.GetAll();
		var memoryViewModels = mapper.Map<IEnumerable<MemoryViewModel>>(memoryDtos);

		foreach (var memoryDto in memoryDtos)
		{
			var memoryViewModel = memoryViewModels.FirstOrDefault(m => m.Id == memoryDto.Id);

			if (memoryViewModel != null)
			{
				memoryViewModel.CanUpdateAndDelete = memoryDto.CanUpdate && memoryDto.CanDelete;
			}
		}

		var modelDtos = await services.Models.GetAll();
		var modelViewModels = mapper.Map<IEnumerable<ModelViewModel>>(modelDtos);

		foreach (var modelDto in modelDtos)
		{
			var modelViewModel = modelViewModels.FirstOrDefault(m => m.Id == modelDto.Id);

			if (modelViewModel != null)
			{
				modelViewModel.CanUpdateAndDelete = modelDto.CanUpdate && modelDto.CanDelete;
			}
		}

		var storageDtos = await services.Storages.GetAll();
		var storageViewModels = mapper.Map<IEnumerable<StorageViewModel>>(storageDtos);

		foreach (var storageDto in storageDtos)
		{
			var storageViewModel = storageViewModels.FirstOrDefault(s => s.Id == storageDto.Id);

			if (storageViewModel != null)
			{
				storageViewModel.CanUpdateAndDelete = storageDto.CanUpdate && storageDto.CanDelete;
			}
		}

		ViewBag.Brands = brandViewModels;
		ViewBag.Cpus = cpuViewModels;
		ViewBag.Displays = displayViewModels;
		ViewBag.Memories = memoryViewModels;
		ViewBag.Models = modelViewModels;
		ViewBag.Storages = storageViewModels;

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
		var brandViewModels = mapper.Map<IEnumerable<BrandViewModel>>(brandDtos);

		foreach (var brandDto in brandDtos)
		{
			var brandViewModel = brandViewModels.FirstOrDefault(b => b.Id == brandDto.Id);

			if (brandViewModel != null)
			{
				brandViewModel.CanUpdateAndDelete = brandDto.CanUpdate && brandDto.CanDelete;
			}
		}

		var cpuDtos = await services.Cpus.GetAll();
		var cpuViewModels = mapper.Map<IEnumerable<CpuViewModel>>(cpuDtos);

		foreach (var cpuDto in cpuDtos)
		{
			var cpuViewModel = cpuViewModels.FirstOrDefault(c => c.Id == cpuDto.Id);

			if (cpuViewModel != null)
			{
				cpuViewModel.CanUpdateAndDelete = cpuDto.CanUpdate && cpuDto.CanDelete;
			}
		}

		var displayDtos = await services.Displays.GetAll();
		var displayViewModels = mapper.Map<IEnumerable<DisplayViewModel>>(displayDtos);

		foreach (var displayDto in displayDtos)
		{
			var displayViewModel = displayViewModels.FirstOrDefault(d => d.Id == displayDto.Id);

			if (displayViewModel != null)
			{
				displayViewModel.CanUpdateAndDelete = displayDto.CanUpdate && displayDto.CanDelete;
			}
		}

		var memoryDtos = await services.Memories.GetAll();
		var memoryViewModels = mapper.Map<IEnumerable<MemoryViewModel>>(memoryDtos);

		foreach (var memoryDto in memoryDtos)
		{
			var memoryViewModel = memoryViewModels.FirstOrDefault(m => m.Id == memoryDto.Id);

			if (memoryViewModel != null)
			{
				memoryViewModel.CanUpdateAndDelete = memoryDto.CanUpdate && memoryDto.CanDelete;
			}
		}

		var modelDtos = await services.Models.GetAll();
		var modelViewModels = mapper.Map<IEnumerable<ModelViewModel>>(modelDtos);

		foreach (var modelDto in modelDtos)
		{
			var modelViewModel = modelViewModels.FirstOrDefault(m => m.Id == modelDto.Id);

			if (modelViewModel != null)
			{
				modelViewModel.CanUpdateAndDelete = modelDto.CanUpdate && modelDto.CanDelete;
			}
		}

		var storageDtos = await services.Storages.GetAll();
		var storageViewModels = mapper.Map<IEnumerable<StorageViewModel>>(storageDtos);

		foreach (var storageDto in storageDtos)
		{
			var storageViewModel = storageViewModels.FirstOrDefault(s => s.Id == storageDto.Id);

			if (storageViewModel != null)
			{
				storageViewModel.CanUpdateAndDelete = storageDto.CanUpdate && storageDto.CanDelete;
			}
		}

		ViewBag.Brands = brandViewModels;
		ViewBag.Cpus = cpuViewModels;
		ViewBag.Displays = displayViewModels;
		ViewBag.Memories = memoryViewModels;
		ViewBag.Models = modelViewModels;
		ViewBag.Storages = storageViewModels;

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
