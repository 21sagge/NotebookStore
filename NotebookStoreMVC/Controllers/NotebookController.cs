using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotebookStore.Entities;
using NotebookStoreMVC.Models;
using NotebookStoreMVC.Repositories;

namespace NotebookStoreMVC.Controllers
{
	public class NotebookController : Controller
	{
		private readonly INotebookRepository _notebookRepository;
		private readonly IMapper mapper;

		public NotebookController(INotebookRepository repository, IMapper mapper)
		{
			_notebookRepository = repository;
			this.mapper = mapper;
		}

		// GET: Notebook
		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var notebookViewModels = await _notebookRepository.Read();
			return View(notebookViewModels);
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
			var model = new NotebookViewModel
			{
				Brands = mapper.Map<IEnumerable<BrandViewModel>>(_notebookRepository.Brands),
				Cpus = mapper.Map<IEnumerable<CpuViewModel>>(_notebookRepository.Cpus),
				Displays = mapper.Map<IEnumerable<DisplayViewModel>>(_notebookRepository.Displays),
				Memories = mapper.Map<IEnumerable<MemoryViewModel>>(_notebookRepository.Memories),
				Models = mapper.Map<IEnumerable<ModelViewModel>>(_notebookRepository.Models),
				Storages = mapper.Map<IEnumerable<StorageViewModel>>(_notebookRepository.Storages)
			};
			return View(model);
		}

		// POST: Notebook/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create([Bind("Id,Color,Price,BrandId,ModelId,CpuId,DisplayId,MemoryId,StorageId")] NotebookViewModel notebook)
		{
			if (ModelState.IsValid)
			{
				_notebookRepository.Create(notebook);
				return RedirectToAction(nameof(Index));
			}
			var model = new NotebookViewModel
			{
				Brands = mapper.Map<IEnumerable<BrandViewModel>>(_notebookRepository.Brands),
				Cpus = mapper.Map<IEnumerable<CpuViewModel>>(_notebookRepository.Cpus),
				Displays = mapper.Map<IEnumerable<DisplayViewModel>>(_notebookRepository.Displays),
				Memories = mapper.Map<IEnumerable<MemoryViewModel>>(_notebookRepository.Memories),
				Models = mapper.Map<IEnumerable<ModelViewModel>>(_notebookRepository.Models),
				Storages = mapper.Map<IEnumerable<StorageViewModel>>(_notebookRepository.Storages)
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
				Brands = _notebookRepository.Brands,
				Cpus = _notebookRepository.Cpus,
				Displays = _notebookRepository.Displays,
				Memories = _notebookRepository.Memories,
				Models = _notebookRepository.Models,
				Storages = _notebookRepository.Storages
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
					await _notebookRepository.Update(notebook);
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
			var model = new NotebookViewModel
			{
				Brands = mapper.Map<IEnumerable<BrandViewModel>>(_notebookRepository.Brands),
				Cpus = mapper.Map<IEnumerable<CpuViewModel>>(_notebookRepository.Cpus),
				Displays = mapper.Map<IEnumerable<DisplayViewModel>>(_notebookRepository.Displays),
				Memories = mapper.Map<IEnumerable<MemoryViewModel>>(_notebookRepository.Memories),
				Models = mapper.Map<IEnumerable<ModelViewModel>>(_notebookRepository.Models),
				Storages = mapper.Map<IEnumerable<StorageViewModel>>(_notebookRepository.Storages)
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

			return View(notebook);
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
}
