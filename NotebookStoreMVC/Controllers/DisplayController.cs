using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotebookStore.Entities;
using NotebookStoreMVC.Repositories;

namespace NotebookStoreMVC.Controllers
{
    public class DisplayController : Controller
    {
        private readonly IRepository<Display> _displayRepository;

        public DisplayController(IRepository<Display> repository)
        {
            _displayRepository = repository;
        }

        // GET: Display
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _displayRepository.Read());
        }

        // GET: Display/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _displayRepository.Read() == null)
            {
                return NotFound();
            }

            var display = await _displayRepository.Find(id);
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
        public IActionResult Create([Bind("Id,Size,ResolutionWidth,ResolutionHeight,PanelType")] Display display)
        {
            if (ModelState.IsValid)
            {
                _displayRepository.Create(display);
                return RedirectToAction(nameof(Index));
            }
            return View(display);
        }

        // GET: Display/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _displayRepository.Read() == null)
            {
                return NotFound();
            }

            var display = await _displayRepository.Find(id);
            if (display == null)
            {
                return NotFound();
            }
            return View(display);
        }

        // POST: Display/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Size,ResolutionWidth,ResolutionHeight,PanelType")] Display display)
        {
            if (id != display.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _displayRepository.Update(display);
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
            if (id == null || _displayRepository.Read() == null)
            {
                return NotFound();
            }

            var display = await _displayRepository.Find(id);
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
            if (_displayRepository.Read() == null)
            {
                return Problem("Entity set 'NotebookStoreContext.Displays'  is null.");
            }
            var display = await _displayRepository.Find(id);
            if (display != null)
            {
                _displayRepository.Delete(display);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool DisplayExists(int id)
        {
            return _displayRepository.Find(id) != null;
        }
    }
}
