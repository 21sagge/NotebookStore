using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotebookStoreMVC.Models;
using NotebookStore.DAL;
using NotebookStore.Entities;

namespace NotebookStoreMVC.Controllers;

public class NotebookController : Controller
{
	private readonly IUnitOfWork unitOfWork;
	private readonly IMapper mapper;

	public NotebookController(IUnitOfWork unitOfWork, IMapper mapper)
	{
		this.unitOfWork = unitOfWork;
		this.mapper = mapper;
	}

	// GET: Notebook
	[HttpGet]
	public async Task<IActionResult> Index()
	{
		unitOfWork.BeginTransaction();

		try
		{
			var notebooks = await unitOfWork.Notebooks.Read();
			unitOfWork.CommitTransaction();
			return View(mapper.Map<IEnumerable<NotebookViewModel>>(notebooks));
		}
		catch (Exception ex)
		{
			unitOfWork.RollbackTransaction();
			return Problem(ex.Message);
		}
	}

	// GET: Notebook/Details/5
	[HttpGet]
	public async Task<IActionResult> Details(int? id)
	{
		if (id == null || unitOfWork.Notebooks.Read() == null)
		{
			return NotFound();
		}

		unitOfWork.BeginTransaction();

		try
		{
			var notebook = await unitOfWork.Notebooks.Find(id);
			unitOfWork.CommitTransaction();

			if (notebook == null)
			{
				return NotFound();
			}

			return View(mapper.Map<NotebookViewModel>(notebook));
		}
		catch (Exception ex)
		{
			unitOfWork.RollbackTransaction();
			return Problem(ex.Message);
		}
	}

	// GET: Notebook/Create
	[HttpGet]
	public async Task<IActionResult> Create()
	{
		unitOfWork.BeginTransaction();

		try
		{
			var notebooks = await unitOfWork.Notebooks.Read();
			unitOfWork.CommitTransaction();

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
		catch (Exception ex)
		{
			unitOfWork.RollbackTransaction();
			return Problem(ex.Message);
		}
	}

	// POST: Notebook/Create
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Create([Bind("Id,Color,Price,BrandId,ModelId,CpuId,DisplayId,MemoryId,StorageId")] NotebookViewModel notebook)
	{
		if (ModelState.IsValid)
		{
			unitOfWork.BeginTransaction();

			try
			{
				await unitOfWork.Notebooks.Create(mapper.Map<Notebook>(notebook));
				unitOfWork.CommitTransaction();
				return RedirectToAction(nameof(Index));
			}
			catch (Exception ex)
			{
				unitOfWork.RollbackTransaction();
				return Problem(ex.Message);
			}
		}

		var notebooks = await unitOfWork.Notebooks.Read();
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
		if (id == null || unitOfWork.Notebooks.Read() == null)
		{
			return NotFound();
		}

		unitOfWork.BeginTransaction();

		try
		{
			var notebook = await unitOfWork.Notebooks.Find(id);
			unitOfWork.CommitTransaction();

			if (notebook == null)
			{
				return NotFound();
			}

			var notebooks = await unitOfWork.Notebooks.Read();
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
		catch (Exception ex)
		{
			unitOfWork.RollbackTransaction();
			return Problem(ex.Message);
		}
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
			unitOfWork.BeginTransaction();

			try
			{
				await unitOfWork.Notebooks.Update(mapper.Map<Notebook>(notebook));
				unitOfWork.CommitTransaction();
				return RedirectToAction(nameof(Index));
			}
			catch (Exception ex)
			{
				unitOfWork.RollbackTransaction();
				return Problem(ex.Message);
			}
		}

		var notebooks = await unitOfWork.Notebooks.Read();
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
		if (id == null || unitOfWork.Notebooks.Read() == null)
		{
			return NotFound();
		}

		unitOfWork.BeginTransaction();

		try
		{
			var notebook = await unitOfWork.Notebooks.Find(id);
			unitOfWork.CommitTransaction();

			if (notebook == null)
			{
				return NotFound();
			}

			return View(mapper.Map<NotebookViewModel>(notebook));
		}
		catch (Exception ex)
		{
			unitOfWork.RollbackTransaction();
			return Problem(ex.Message);
		}
	}

	// POST: Notebook/Delete/5
	[HttpPost, ActionName("Delete")]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> DeleteConfirmed(int id)
	{
		if (unitOfWork.Notebooks.Read() == null)
		{
			return NotFound();
		}

		unitOfWork.BeginTransaction();

		try
		{
			await unitOfWork.Notebooks.Delete(id);
			unitOfWork.CommitTransaction();
			return RedirectToAction(nameof(Index));
		}
		catch (Exception ex)
		{
			unitOfWork.RollbackTransaction();
			return Problem(ex.Message);
		}
	}

	private bool NotebookExists(int id)
	{
		return unitOfWork.Notebooks.Find(id) != null;
	}
}
