using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotebookStoreMVC.Models;
using NotebookStore.DAL;
using NotebookStore.Entities;

namespace NotebookStoreMVC.Controllers;

public class NotebookController : Controller
{
	private readonly IRepository<Notebook> _notebookRepository;
	private readonly IMapper mapper;

	public NotebookController(IRepository<Notebook> repository, IMapper mapper)
	{
		_notebookRepository = repository;
		this.mapper = mapper;
	}

	// GET: Notebook
	[HttpGet]
	public async Task<IActionResult> Index()
	{
		var notebookViewModels = await _notebookRepository.Read();
		var mapped = mapper.Map<IEnumerable<NotebookViewModel>>(notebookViewModels);

		return View(mapped);
	}

	// GET: Notebook/Details/5
	[HttpGet]
	public async Task<IActionResult> Details(int? id)
	{
		if (id == null || _notebookRepository.Read() == null)
		{
			return NotFound();
		}

		var notebook = await _notebookRepository.Find(id);
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
		var notebooks = await _notebookRepository.Read();

		var model = new NotebookViewModel
		{
			Brands = mapper.Map<IEnumerable<BrandViewModel>>(notebooks.Select(n => n.Brand)),
			Cpus = mapper.Map<IEnumerable<CpuViewModel>>(notebooks.Select(n => n.Cpu)),
			Displays = mapper.Map<IEnumerable<DisplayViewModel>>(notebooks.Select(n => n.Display)),
			Memories = mapper.Map<IEnumerable<MemoryViewModel>>(notebooks.Select(n => n.Memory)),
			Models = mapper.Map<IEnumerable<ModelViewModel>>(notebooks.Select(n => n.Model)),
			Storages = mapper.Map<IEnumerable<StorageViewModel>>(notebooks.Select(n => n.Storage))
		};
		return View(model);
	}

	// POST: Notebook/Create
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Create([Bind("Id,Color,Price,BrandId,ModelId,CpuId,DisplayId,MemoryId,StorageId")] NotebookViewModel notebook)
	{
		if (ModelState.IsValid)
		{
			await _notebookRepository.Create(mapper.Map<Notebook>(notebook));
			return RedirectToAction(nameof(Index));
		}

		var notebooks = await _notebookRepository.Read();
		var model = new NotebookViewModel
		{
			Brands = mapper.Map<IEnumerable<BrandViewModel>>(notebooks.Select(n => n.Brand)),
			Cpus = mapper.Map<IEnumerable<CpuViewModel>>(notebooks.Select(n => n.Cpu)),
			Displays = mapper.Map<IEnumerable<DisplayViewModel>>(notebooks.Select(n => n.Display)),
			Memories = mapper.Map<IEnumerable<MemoryViewModel>>(notebooks.Select(n => n.Memory)),
			Models = mapper.Map<IEnumerable<ModelViewModel>>(notebooks.Select(n => n.Model)),
			Storages = mapper.Map<IEnumerable<StorageViewModel>>(notebooks.Select(n => n.Storage))
		};
		return View(model);
	}

	// GET: Notebook/Edit/5
	[HttpGet]
	public async Task<IActionResult> Edit(int? id)
	{
		if (id == null || _notebookRepository.Read() == null)
		{
			return NotFound();
		}

		var notebook = await _notebookRepository.Find(id);
		if (notebook == null)
		{
			return NotFound();
		}

		var notebooks = await _notebookRepository.Read();

		var model = new NotebookViewModel
		{
			Color = notebook.Color,
			Price = notebook.Price,
			BrandId = notebook.BrandId,
			ModelId = notebook.ModelId,
			CpuId = notebook.CpuId,
			DisplayId = notebook.DisplayId,
			MemoryId = notebook.MemoryId,
			StorageId = notebook.StorageId,
			Brands = mapper.Map<IEnumerable<BrandViewModel>>(notebooks.Select(n => n.Brand)),
			Cpus = mapper.Map<IEnumerable<CpuViewModel>>(notebooks.Select(n => n.Cpu)),
			Displays = mapper.Map<IEnumerable<DisplayViewModel>>(notebooks.Select(n => n.Display)),
			Memories = mapper.Map<IEnumerable<MemoryViewModel>>(notebooks.Select(n => n.Memory)),
			Models = mapper.Map<IEnumerable<ModelViewModel>>(notebooks.Select(n => n.Model)),
			Storages = mapper.Map<IEnumerable<StorageViewModel>>(notebooks.Select(n => n.Storage))
		};

		return View(model);
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
			try
			{
				await _notebookRepository.Update(mapper.Map<Notebook>(notebook));
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!NotebookExists(notebook.Id))
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

		var notebooks = await _notebookRepository.Read();
		var model = new NotebookViewModel
		{
			Brands = mapper.Map<IEnumerable<BrandViewModel>>(notebooks.Select(n => n.Brand)),
			Cpus = mapper.Map<IEnumerable<CpuViewModel>>(notebooks.Select(n => n.Cpu)),
			Displays = mapper.Map<IEnumerable<DisplayViewModel>>(notebooks.Select(n => n.Display)),
			Memories = mapper.Map<IEnumerable<MemoryViewModel>>(notebooks.Select(n => n.Memory)),
			Models = mapper.Map<IEnumerable<ModelViewModel>>(notebooks.Select(n => n.Model)),
			Storages = mapper.Map<IEnumerable<StorageViewModel>>(notebooks.Select(n => n.Storage))
		};
		return View(model);
	}

	// GET: Notebook/Delete/5
	[HttpGet]
	public async Task<IActionResult> Delete(int? id)
	{
		if (id == null || _notebookRepository.Read() == null)
		{
			return NotFound();
		}

		var notebook = await _notebookRepository.Find(id);
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
		if (_notebookRepository.Read() == null)
		{
			return Problem("Entity set 'NotebookStoreContext.Notebook'  is null.");
		}

		await _notebookRepository.Delete(id);
		return RedirectToAction(nameof(Index));
	}

	private bool NotebookExists(int id)
	{
		return _notebookRepository.Find(id) != null;
	}
}
