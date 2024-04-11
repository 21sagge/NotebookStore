using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotebookStore.Entities;
using NotebookStoreMVC.Repositories;

namespace NotebookStoreMVC.Controllers
{
    public class ModelController : Controller
    {
        private readonly IRepository<Model> _modelRepository;

        public ModelController(IRepository<Model> repository)
        {
            _modelRepository = repository;
        }

        // GET: Model
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _modelRepository.Read());
        }

        // GET: Model/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _modelRepository.Read() == null)
            {
                return NotFound();
            }

            var model = await _modelRepository.Find(id);
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // GET: Model/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Model/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name")] Model model)
        {
            if (ModelState.IsValid)
            {
                _modelRepository.Create(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Model/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _modelRepository.Read() == null)
            {
                return NotFound();
            }

            var model = await _modelRepository.Find(id);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        // POST: Model/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name")] Model model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _modelRepository.Update(model);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModelExists(model.Id))
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
            return View(model);
        }

        // GET: Model/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _modelRepository.Read() == null)
            {
                return NotFound();
            }

            var model = await _modelRepository.Find(id);
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // POST: Model/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_modelRepository.Read() == null)
            {
                return Problem("Entity set 'NotebookStoreContext.Models'  is null.");
            }
            var model = await _modelRepository.Find(id);
            if (model != null)
            {
                _modelRepository.Delete(model);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ModelExists(int id)
        {
            return _modelRepository.Find(id) != null;
        }
    }
}
