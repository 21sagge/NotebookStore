using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NotebookStore.Entities;
using NotebookStoreContext;

namespace NotebookStoreMVC.Controllers
{
    public class StorageController : Controller
    {
        private readonly NotebookStoreContext.NotebookStoreContext _context;

        public StorageController(NotebookStoreContext.NotebookStoreContext context)
        {
            _context = context;
        }

        // GET: Storage
        public async Task<IActionResult> Index()
        {
              return _context.Storages != null ? 
                          View(await _context.Storages.ToListAsync()) :
                          Problem("Entity set 'NotebookStoreContext.Storages'  is null.");
        }

        // GET: Storage/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Storages == null)
            {
                return NotFound();
            }

            var storage = await _context.Storages
                .FirstOrDefaultAsync(m => m.Id == id);
            if (storage == null)
            {
                return NotFound();
            }

            return View(storage);
        }

        // GET: Storage/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Storage/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Type,Capacity")] Storage storage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(storage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(storage);
        }

        // GET: Storage/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Storages == null)
            {
                return NotFound();
            }

            var storage = await _context.Storages.FindAsync(id);
            if (storage == null)
            {
                return NotFound();
            }
            return View(storage);
        }

        // POST: Storage/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Type,Capacity")] Storage storage)
        {
            if (id != storage.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(storage);
                    await _context.SaveChangesAsync();
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
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Storages == null)
            {
                return NotFound();
            }

            var storage = await _context.Storages
                .FirstOrDefaultAsync(m => m.Id == id);
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
            if (_context.Storages == null)
            {
                return Problem("Entity set 'NotebookStoreContext.Storages'  is null.");
            }
            var storage = await _context.Storages.FindAsync(id);
            if (storage != null)
            {
                _context.Storages.Remove(storage);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StorageExists(int id)
        {
          return (_context.Storages?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
