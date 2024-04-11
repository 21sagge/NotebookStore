using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotebookStore.Entities;
using NotebookStoreMVC.Repositories;

namespace NotebookStoreMVC.Controllers
{
    public class BrandController : Controller
    {
        private readonly IRepository<Brand> _brandRepository;

        public BrandController(IRepository<Brand> brandRepository)
        {
            _brandRepository = brandRepository;
        }

        // GET: Brand
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _brandRepository.Read());
        }

        // GET: Brand/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _brandRepository.Read() == null)
            {
                return NotFound();
            }

            var brand = await _brandRepository.Find(id);
            if (brand == null)
            {
                return NotFound();
            }

            return View(brand);
        }

        // GET: Brand/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Brand/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name")] Brand brand)
        {
            if (ModelState.IsValid)
            {
                _brandRepository.Create(brand);
                return RedirectToAction(nameof(Index));
            }
            return View(brand);
        }

        // GET: Brand/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _brandRepository.Read() == null)
            {
                return NotFound();
            }

            var brand = await _brandRepository.Find(id);
            if (brand == null)
            {
                return NotFound();
            }
            return View(brand);
        }

        // POST: Brand/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name")] Brand brand)
        {
            if (id != brand.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _brandRepository.Update(brand);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BrandExists(brand.Id))
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
            return View(brand);
        }

        // GET: Brand/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _brandRepository.Read() == null)
            {
                return NotFound();
            }

            var brand = await _brandRepository.Find(id);
            if (brand == null)
            {
                return NotFound();
            }

            return View(brand);
        }

        // POST: Brand/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_brandRepository.Read() == null)
            {
                return Problem("Entity set 'NotebookStoreContext.Brands'  is null.");
            }
            var brand = await _brandRepository.Find(id);
            if (brand != null)
            {
                _brandRepository.Delete(brand);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool BrandExists(int id)
        {
            return _brandRepository.Find(id) != null;
        }
    }
}
