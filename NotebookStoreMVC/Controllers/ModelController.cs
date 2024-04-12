using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotebookStore.Entities;
using NotebookStoreMVC.Models;
using NotebookStoreMVC.Repositories;

namespace NotebookStoreMVC.Controllers
{
    public class ModelController : Controller
    {
        private readonly IRepository<ModelViewModel> _modelRepository;
        private readonly IMapper mapper;

        public ModelController(IRepository<ModelViewModel> repository, IMapper mapper)
        {
            _modelRepository = repository;
            this.mapper = mapper;
        }

        // GET: ModelViewModel
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var modelViewModels = await _modelRepository.Read();
            var models = modelViewModels.Select(bvm => mapper.Map<Model>(bvm));
            return View(models);
        }

        // GET: ModelViewModel/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _modelRepository.Read() == null)
            {
                return NotFound();
            }

            var ModelViewModel = await _modelRepository.Find(id);
            if (ModelViewModel == null)
            {
                return NotFound();
            }

            return View(mapper.Map<Model>(ModelViewModel));
        }

        // GET: ModelViewModel/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: ModelViewModel/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name")] ModelViewModel ModelViewModel)
        {
            if (ModelState.IsValid)
            {
                _modelRepository.Create(ModelViewModel);
                return RedirectToAction(nameof(Index));
            }
            return View(ModelViewModel);
        }

        // GET: ModelViewModel/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _modelRepository.Read() == null)
            {
                return NotFound();
            }

            var ModelViewModel = await _modelRepository.Find(id);
            if (ModelViewModel == null)
            {
                return NotFound();
            }
            return View(mapper.Map<Model>(ModelViewModel));
        }

        // POST: ModelViewModel/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name")] ModelViewModel ModelViewModel)
        {
            if (id != ModelViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _modelRepository.Update(ModelViewModel);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModelExists(ModelViewModel.Id))
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
            return View(ModelViewModel);
        }

        // GET: ModelViewModel/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _modelRepository.Read() == null)
            {
                return NotFound();
            }

            var ModelViewModel = await _modelRepository.Find(id);
            if (ModelViewModel == null)
            {
                return NotFound();
            }

            return View(mapper.Map<Model>(ModelViewModel));
        }

        // POST: ModelViewModel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_modelRepository.Read() == null)
            {
                return Problem("Entity set 'NotebookStoreContext.Model'  is null.");
            }

            await _modelRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool ModelExists(int id)
        {
            return _modelRepository.Find(id) != null;
        }
    }
}
