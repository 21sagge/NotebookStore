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
    public class MemoryController : Controller
    {
        private readonly NotebookStoreContext.NotebookStoreContext _context;

        public MemoryController(NotebookStoreContext.NotebookStoreContext context)
        {
            _context = context;
        }

        // GET: Memory
        public async Task<IActionResult> Index()
        {
            return _context.Memories != null ?
                        View(await _context.Memories.ToListAsync()) :
                        Problem("Entity set 'NotebookStoreContext.Memories'  is null.");
        }

        // GET: Memory/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Memories == null)
            {
                return NotFound();
            }

            var memory = await _context.Memories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (memory == null)
            {
                return NotFound();
            }

            return View(memory);
        }

        // GET: Memory/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Memory/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Capacity,Speed")] Memory memory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(memory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(memory);
        }

        // GET: Memory/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Memories == null)
            {
                return NotFound();
            }

            var memory = await _context.Memories.FindAsync(id);
            if (memory == null)
            {
                return NotFound();
            }
            return View(memory);
        }

        // POST: Memory/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Capacity,Speed")] Memory memory)
        {
            if (id != memory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(memory);
                    await _context.SaveChangesAsync();
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
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Memories == null)
            {
                return NotFound();
            }

            var memory = await _context.Memories
                .FirstOrDefaultAsync(m => m.Id == id);
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
            if (_context.Memories == null)
            {
                return Problem("Entity set 'NotebookStoreContext.Memories'  is null.");
            }
            var memory = await _context.Memories.FindAsync(id);
            if (memory != null)
            {
                _context.Memories.Remove(memory);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MemoryExists(int id)
        {
            return (_context.Memories?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
