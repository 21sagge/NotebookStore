using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotebookStore.Entities;

namespace NotebookStoreMVC.Controllers
{
    public class CpuController : Controller
    {
        private readonly NotebookStoreContext.NotebookStoreContext _context;

        public CpuController(NotebookStoreContext.NotebookStoreContext context)
        {
            _context = context;
        }

        // GET: Cpu
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return _context.Cpus != null ?
                        View(await _context.Cpus.ToListAsync()) :
                        Problem("Entity set 'NotebookStoreContext.Cpus'  is null.");
        }

        // GET: Cpu/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Cpus == null)
            {
                return NotFound();
            }

            var cpu = await _context.Cpus
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cpu == null)
            {
                return NotFound();
            }

            return View(cpu);
        }

        // GET: Cpu/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cpu/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Brand,Model")] Cpu cpu)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cpu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cpu);
        }

        // GET: Cpu/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Cpus == null)
            {
                return NotFound();
            }

            var cpu = await _context.Cpus.FindAsync(id);
            if (cpu == null)
            {
                return NotFound();
            }
            return View(cpu);
        }

        // POST: Cpu/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Brand,Model")] Cpu cpu)
        {
            if (id != cpu.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cpu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CpuExists(cpu.Id))
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
            return View(cpu);
        }

        // GET: Cpu/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Cpus == null)
            {
                return NotFound();
            }

            var cpu = await _context.Cpus
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cpu == null)
            {
                return NotFound();
            }

            return View(cpu);
        }

        // POST: Cpu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Cpus == null)
            {
                return Problem("Entity set 'NotebookStoreContext.Cpus'  is null.");
            }
            var cpu = await _context.Cpus.FindAsync(id);
            if (cpu != null)
            {
                _context.Cpus.Remove(cpu);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CpuExists(int id)
        {
            return (_context.Cpus?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
