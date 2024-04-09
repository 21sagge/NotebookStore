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
    public class NotebookController : Controller
    {
        private readonly NotebookStoreContext.NotebookStoreContext _context;

        public NotebookController(NotebookStoreContext.NotebookStoreContext context)
        {
            _context = context;
        }

        // GET: Notebook
        public async Task<IActionResult> Index()
        {
            var notebookStoreContext = _context.Notebooks.Include(n => n.Brand).Include(n => n.Cpu).Include(n => n.Display).Include(n => n.Memory).Include(n => n.Model).Include(n => n.Storage);
            return View(await notebookStoreContext.ToListAsync());
        }

        // GET: Notebook/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Notebooks == null)
            {
                return NotFound();
            }

            var notebook = await _context.Notebooks
                .Include(n => n.Brand)
                .Include(n => n.Cpu)
                .Include(n => n.Display)
                .Include(n => n.Memory)
                .Include(n => n.Model)
                .Include(n => n.Storage)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (notebook == null)
            {
                return NotFound();
            }

            return View(notebook);
        }

        // GET: Notebook/Create
        public IActionResult Create()
        {
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Name");
            ViewData["CpuId"] = new SelectList(_context.Cpus, "Id", "Brand");
            ViewData["DisplayId"] = new SelectList(_context.Displays, "Id", "PanelType");
            ViewData["MemoryId"] = new SelectList(_context.Memories, "Id", "Id");
            ViewData["ModelId"] = new SelectList(_context.Models, "Id", "Name");
            ViewData["StorageId"] = new SelectList(_context.Storages, "Id", "Type");
            return View();
        }

        // POST: Notebook/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Color,Price,BrandId,ModelId,CpuId,DisplayId,MemoryId,StorageId")] Notebook notebook)
        {
            if (ModelState.IsValid)
            {
                _context.Add(notebook);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Name", notebook.BrandId);
            ViewData["CpuId"] = new SelectList(_context.Cpus, "Id", "Brand", notebook.CpuId);
            ViewData["DisplayId"] = new SelectList(_context.Displays, "Id", "PanelType", notebook.DisplayId);
            ViewData["MemoryId"] = new SelectList(_context.Memories, "Id", "Id", notebook.MemoryId);
            ViewData["ModelId"] = new SelectList(_context.Models, "Id", "Name", notebook.ModelId);
            ViewData["StorageId"] = new SelectList(_context.Storages, "Id", "Type", notebook.StorageId);
            return View(notebook);
        }

        // GET: Notebook/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Notebooks == null)
            {
                return NotFound();
            }

            var notebook = await _context.Notebooks.FindAsync(id);
            if (notebook == null)
            {
                return NotFound();
            }
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Name", notebook.BrandId);
            ViewData["CpuId"] = new SelectList(_context.Cpus, "Id", "Brand", notebook.CpuId);
            ViewData["DisplayId"] = new SelectList(_context.Displays, "Id", "PanelType", notebook.DisplayId);
            ViewData["MemoryId"] = new SelectList(_context.Memories, "Id", "Id", notebook.MemoryId);
            ViewData["ModelId"] = new SelectList(_context.Models, "Id", "Name", notebook.ModelId);
            ViewData["StorageId"] = new SelectList(_context.Storages, "Id", "Type", notebook.StorageId);
            return View(notebook);
        }

        // POST: Notebook/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Color,Price,BrandId,ModelId,CpuId,DisplayId,MemoryId,StorageId")] Notebook notebook)
        {
            if (id != notebook.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(notebook);
                    await _context.SaveChangesAsync();
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
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Name", notebook.BrandId);
            ViewData["CpuId"] = new SelectList(_context.Cpus, "Id", "Brand", notebook.CpuId);
            ViewData["DisplayId"] = new SelectList(_context.Displays, "Id", "PanelType", notebook.DisplayId);
            ViewData["MemoryId"] = new SelectList(_context.Memories, "Id", "Id", notebook.MemoryId);
            ViewData["ModelId"] = new SelectList(_context.Models, "Id", "Name", notebook.ModelId);
            ViewData["StorageId"] = new SelectList(_context.Storages, "Id", "Type", notebook.StorageId);
            return View(notebook);
        }

        // GET: Notebook/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Notebooks == null)
            {
                return NotFound();
            }

            var notebook = await _context.Notebooks
                .Include(n => n.Brand)
                .Include(n => n.Cpu)
                .Include(n => n.Display)
                .Include(n => n.Memory)
                .Include(n => n.Model)
                .Include(n => n.Storage)
                .FirstOrDefaultAsync(m => m.Id == id);
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
            if (_context.Notebooks == null)
            {
                return Problem("Entity set 'NotebookStoreContext.Notebooks'  is null.");
            }
            var notebook = await _context.Notebooks.FindAsync(id);
            if (notebook != null)
            {
                _context.Notebooks.Remove(notebook);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NotebookExists(int id)
        {
          return (_context.Notebooks?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
