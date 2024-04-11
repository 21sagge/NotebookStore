using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotebookStore.Entities;
using NotebookStoreMVC.Repositories;

namespace NotebookStoreMVC.Controllers
{
    public class MemoryController : Controller
    {
        private readonly IRepository<Memory> _memoryRepository;

        public MemoryController(IRepository<Memory> repository)
        {
            _memoryRepository = repository;
        }

        // GET: Memory
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _memoryRepository.Read());
        }

        // GET: Memory/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _memoryRepository.Read() == null)
            {
                return NotFound();
            }

            var memory = await _memoryRepository.Find(id);
            if (memory == null)
            {
                return NotFound();
            }

            return View(memory);
        }

        // GET: Memory/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Memory/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Capacity,Speed")] Memory memory)
        {
            if (ModelState.IsValid)
            {
                _memoryRepository.Create(memory);
                return RedirectToAction(nameof(Index));
            }
            return View(memory);
        }

        // GET: Memory/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _memoryRepository.Read() == null)
            {
                return NotFound();
            }

            var memory = await _memoryRepository.Find(id);
            if (memory == null)
            {
                return NotFound();
            }
            return View(memory);
        }

        // POST: Memory/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Capacity,Speed")] Memory memory)
        {
            if (id != memory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _memoryRepository.Update(memory);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemoryExists(memory.Id))
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
            return View(memory);
        }

        // GET: Memory/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _memoryRepository.Read() == null)
            {
                return NotFound();
            }

            var memory = await _memoryRepository.Find(id);
            if (memory == null)
            {
                return NotFound();
            }

            return View(memory);
        }

        // POST: Memory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_memoryRepository.Read() == null)
            {
                return Problem("Entity set 'NotebookStoreContext.Memories'  is null.");
            }
            var memory = await _memoryRepository.Find(id);
            if (memory != null)
            {
                _memoryRepository.Delete(memory);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool MemoryExists(int id)
        {
            return _memoryRepository.Find(id) != null;
        }
    }
}
