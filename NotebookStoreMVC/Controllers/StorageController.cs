using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotebookStore.Entities;
using NotebookStoreMVC.Repositories;

namespace NotebookStoreMVC.Controllers
{
    public class StorageController : Controller
    {
        private readonly IRepository<Storage> _storageRepository;

        public StorageController(IRepository<Storage> repository)
        {
            _storageRepository = repository;
        }

        // GET: Storage
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _storageRepository.Read());
        }

        // GET: Storage/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _storageRepository.Read() == null)
            {
                return NotFound();
            }

            var storage = await _storageRepository.Find(id);
            if (storage == null)
            {
                return NotFound();
            }

            return View(storage);
        }

        // GET: Storage/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Storage/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Type,Capacity")] Storage storage)
        {
            if (ModelState.IsValid)
            {
                _storageRepository.Create(storage);
                return RedirectToAction(nameof(Index));
            }
            return View(storage);
        }

        // GET: Storage/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _storageRepository.Read() == null)
            {
                return NotFound();
            }

            var storage = await _storageRepository.Find(id);
            if (storage == null)
            {
                return NotFound();
            }
            return View(storage);
        }

        // POST: Storage/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Type,Capacity")] Storage storage)
        {
            if (id != storage.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _storageRepository.Update(storage);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StorageExists(storage.Id))
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
            return View(storage);
        }

        // GET: Storage/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _storageRepository.Read() == null)
            {
                return NotFound();
            }

            var storage = await _storageRepository.Find(id);
            if (storage == null)
            {
                return NotFound();
            }

            return View(storage);
        }

        // POST: Storage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_storageRepository.Read() == null)
            {
                return Problem("Entity set 'NotebookStoreContext.Storages'  is null.");
            }
            var storage = await _storageRepository.Find(id);
            if (storage != null)
            {
                _storageRepository.Delete(storage);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool StorageExists(int id)
        {
            return _storageRepository.Find(id) != null;
        }
    }
}
