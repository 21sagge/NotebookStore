using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotebookStore.Entities;

namespace NotebookStoreMVC.Controllers
{
    public class DisplayController : Controller
    {
        private readonly NotebookStoreContext.NotebookStoreContext _context;

        public DisplayController(NotebookStoreContext.NotebookStoreContext context)
        {
            _context = context;
        }

        // GET: Display
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return _context.Displays != null ?
                        View(await _context.Displays.ToListAsync()) :
                        Problem("Entity set 'NotebookStoreContext.Displays'  is null.");
        }

        // GET: Display/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Displays == null)
            {
                return NotFound();
            }

            var display = await _context.Displays
                .FirstOrDefaultAsync(m => m.Id == id);
            if (display == null)
            {
                return NotFound();
            }

            return View(display);
        }

        // GET: Display/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Display/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Size,ResolutionWidth,ResolutionHeight,PanelType")] Display display)
        {
            if (ModelState.IsValid)
            {
                _context.Add(display);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(display);
        }

        // GET: Display/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Displays == null)
            {
                return NotFound();
            }

            var display = await _context.Displays.FindAsync(id);
            if (display == null)
            {
                return NotFound();
            }
            return View(display);
        }

        // POST: Display/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Size,ResolutionWidth,ResolutionHeight,PanelType")] Display display)
        {
            if (id != display.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(display);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DisplayExists(display.Id))
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
            return View(display);
        }

        // GET: Display/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Displays == null)
            {
                return NotFound();
            }

            var display = await _context.Displays
                .FirstOrDefaultAsync(m => m.Id == id);
            if (display == null)
            {
                return NotFound();
            }

            return View(display);
        }

        // POST: Display/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Displays == null)
            {
                return Problem("Entity set 'NotebookStoreContext.Displays'  is null.");
            }
            var display = await _context.Displays.FindAsync(id);
            if (display != null)
            {
                _context.Displays.Remove(display);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DisplayExists(int id)
        {
            return (_context.Displays?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
