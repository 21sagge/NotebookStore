using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotebookStore.Entities;
using NotebookStoreMVC.Repositories;

namespace NotebookStoreMVC.Controllers
{
    public class CpuController : Controller
    {
        private readonly IRepository<Cpu> _cpuRepository;

        public CpuController(IRepository<Cpu> repository)
        {
            _cpuRepository = repository;
        }

        // GET: Cpu
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _cpuRepository.Read());
        }

        // GET: Cpu/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _cpuRepository.Read() == null)
            {
                return NotFound();
            }

            var cpu = await _cpuRepository.Find(id);
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
        public IActionResult Create([Bind("Id,Brand,Model")] Cpu cpu)
        {
            if (ModelState.IsValid)
            {
                _cpuRepository.Create(cpu);
                return RedirectToAction(nameof(Index));
            }
            return View(cpu);
        }

        // GET: Cpu/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _cpuRepository.Read() == null)
            {
                return NotFound();
            }

            var cpu = await _cpuRepository.Find(id);
            if (cpu == null)
            {
                return NotFound();
            }
            return View(cpu);
        }

        // POST: Cpu/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Brand,Model")] Cpu cpu)
        {
            if (id != cpu.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _cpuRepository.Update(cpu);
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
            if (id == null || _cpuRepository.Read() == null)
            {
                return NotFound();
            }

            var cpu = await _cpuRepository.Find(id);
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
            if (_cpuRepository.Read() == null)
            {
                return Problem("Entity set 'NotebookStoreContext.Cpus'  is null.");
            }
            var cpu = await _cpuRepository.Find(id);
            if (cpu != null)
            {
                _cpuRepository.Delete(cpu);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool CpuExists(int id)
        {
            return _cpuRepository.Find(id) != null;
        }
    }
}
