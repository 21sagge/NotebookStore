using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NotebookStore.Entities;
using NotebookStoreMVC.Repositories;

namespace NotebookStoreMVC.Controllers
{
	public class NotebookController : Controller
	{
		private readonly INotebookRepository _notebookRepository;

		public NotebookController(INotebookRepository repository)
		{
			_notebookRepository = repository;
		}

		// GET: Notebook
		[HttpGet]
		public async Task<IActionResult> Index()
		{
			return View(await _notebookRepository.Read());
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

			return View(notebook);
		}

		// GET: Notebook/Create
		[HttpGet]
		public IActionResult Create()
		{
			ViewData["BrandId"] = new SelectList(_notebookRepository.Brands, "Id", "Name");
			ViewData["CpuId"] = new SelectList(_notebookRepository.Cpus, "Id", "Name");
			ViewData["DisplayId"] = new SelectList(_notebookRepository.Displays, "Id", "Name");
			ViewData["MemoryId"] = new SelectList(_notebookRepository.Memories, "Id", "CapacityAndSpeed");
			ViewData["ModelId"] = new SelectList(_notebookRepository.Models, "Id", "Name");
			ViewData["StorageId"] = new SelectList(_notebookRepository.Storages, "Id", "TypeAndCapacity");
			return View();
		}

		// POST: Notebook/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create([Bind("Id,Color,Price,BrandId,ModelId,CpuId,DisplayId,MemoryId,StorageId")] Notebook notebook)
		{
			if (ModelState.IsValid)
			{
				_notebookRepository.Create(notebook);
				return RedirectToAction(nameof(Index));
			}
			ViewData["BrandId"] = new SelectList(_notebookRepository.Brands, "Id", "Name", notebook.BrandId);
			ViewData["CpuId"] = new SelectList(_notebookRepository.Cpus, "Id", "Brand", notebook.CpuId);
			ViewData["DisplayId"] = new SelectList(_notebookRepository.Displays, "Id", "PanelType", notebook.DisplayId);
			ViewData["MemoryId"] = new SelectList(_notebookRepository.Memories, "Id", "Id", notebook.MemoryId);
			ViewData["ModelId"] = new SelectList(_notebookRepository.Models, "Id", "Name", notebook.ModelId);
			ViewData["StorageId"] = new SelectList(_notebookRepository.Storages, "Id", "Type", notebook.StorageId);
			return View(notebook);
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
			ViewData["BrandId"] = new SelectList(_notebookRepository.Brands, "Id", "Name", notebook.BrandId);
			ViewData["CpuId"] = new SelectList(_notebookRepository.Cpus, "Id", "Name", notebook.CpuId);
			ViewData["DisplayId"] = new SelectList(_notebookRepository.Displays, "Id", "Name", notebook.DisplayId);
			ViewData["MemoryId"] = new SelectList(_notebookRepository.Memories, "Id", "CapacityAndSpeed", notebook.MemoryId);
			ViewData["ModelId"] = new SelectList(_notebookRepository.Models, "Id", "Name", notebook.ModelId);
			ViewData["StorageId"] = new SelectList(_notebookRepository.Storages, "Id", "TypeAndCapacity", notebook.StorageId);
			return View(notebook);
		}

		// POST: Notebook/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(int id, [Bind("Id,Color,Price,BrandId,ModelId,CpuId,DisplayId,MemoryId,StorageId")] Notebook notebook)
		{
			if (id != notebook.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_notebookRepository.Update(notebook);
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
			ViewData["BrandId"] = new SelectList(_notebookRepository.Brands, "Id", "Name", notebook.BrandId);
			ViewData["CpuId"] = new SelectList(_notebookRepository.Cpus, "Id", "Brand", notebook.CpuId);
			ViewData["DisplayId"] = new SelectList(_notebookRepository.Displays, "Id", "PanelType", notebook.DisplayId);
			ViewData["MemoryId"] = new SelectList(_notebookRepository.Memories, "Id", "Id", notebook.MemoryId);
			ViewData["ModelId"] = new SelectList(_notebookRepository.Models, "Id", "Name", notebook.ModelId);
			ViewData["StorageId"] = new SelectList(_notebookRepository.Storages, "Id", "Type", notebook.StorageId);
			return View(notebook);
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

			return View(notebook);
		}

		// POST: Notebook/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			if (_notebookRepository.Read() == null)
			{
				return Problem("Entity set 'NotebookStoreContext.Notebooks'  is null.");
			}
			var notebook = await _notebookRepository.Find(id);
			if (notebook != null)
			{
				_notebookRepository.Delete(notebook);
			}

			return RedirectToAction(nameof(Index));
		}

		private bool NotebookExists(int id)
		{
			return _notebookRepository.Find(id) != null;
		}
	}
}
