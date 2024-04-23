using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotebookStore.DAL;
using NotebookStore.Entities;

namespace NotebookStoreMVC.Controllers;

public class UserController : Controller
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public UserController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    // GET: userViewModel
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        unitOfWork.BeginTransaction();

        try
        {
            var users = await unitOfWork.Users.Read();
            unitOfWork.CommitTransaction();
            return View(mapper.Map<IEnumerable<UserViewModel>>(users));
        }
        catch (Exception ex)
        {
            unitOfWork.RollbackTransaction();
            return Problem(ex.Message);
        }
    }

    // GET: userViewModel/Details/5
    [HttpGet]
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || unitOfWork.Users.Read() == null)
        {
            return NotFound();
        }

        unitOfWork.BeginTransaction();

        try
        {
            var userViewModel = await unitOfWork.Users.Find(id);
            unitOfWork.CommitTransaction();

            if (userViewModel == null)
            {
                return NotFound();
            }

            return View(mapper.Map<UserViewModel>(userViewModel));
        }
        catch (Exception ex)
        {
            unitOfWork.RollbackTransaction();
            return Problem(ex.Message);
        }
    }

    // GET: userViewModel/Create
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    // POST: userViewModel/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([Bind("Id,Name")] UserViewModel userViewModel)
    {
        if (ModelState.IsValid)
        {
            unitOfWork.BeginTransaction();

            try
            {
                unitOfWork.Users.Create(mapper.Map<User>(userViewModel));
                unitOfWork.CommitTransaction();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                unitOfWork.RollbackTransaction();
                return Problem(ex.Message);
            }
        }
        return View(userViewModel);
    }

    // GET: userViewModel/Edit/5
    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || unitOfWork.Users.Read() == null)
        {
            return NotFound();
        }

        unitOfWork.BeginTransaction();

        try
        {
            var userViewModel = await unitOfWork.Users.Find(id);
            unitOfWork.CommitTransaction();

            if (userViewModel == null)
            {
                return NotFound();
            }

            return View(mapper.Map<UserViewModel>(userViewModel));
        }
        catch (Exception ex)
        {
            unitOfWork.RollbackTransaction();
            return Problem(ex.Message);
        }
    }

    // POST: userViewModel/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, [Bind("Id,Name")] UserViewModel userViewModel)
    {
        if (id != userViewModel.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            unitOfWork.BeginTransaction();

            try
            {
                try
                {
                    unitOfWork.Users.Update(mapper.Map<User>(userViewModel));
                    unitOfWork.CommitTransaction();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(userViewModel.Id))
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
            catch (Exception ex)
            {
                unitOfWork.RollbackTransaction();
                return Problem(ex.Message);
            }
        }
        return View(userViewModel);
    }

    // GET: userViewModel/Delete/5
    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || unitOfWork.Users.Read() == null)
        {
            return NotFound();
        }

        unitOfWork.BeginTransaction();

        try
        {
            var userViewModel = await unitOfWork.Users.Find(id);
            unitOfWork.CommitTransaction();

            if (userViewModel == null)
            {
                return NotFound();
            }

            return View(mapper.Map<UserViewModel>(userViewModel));
        }
        catch (Exception ex)
        {
            unitOfWork.RollbackTransaction();
            return Problem(ex.Message);
        }
    }

    // POST: userViewModel/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (unitOfWork.Users.Read() == null)
        {
            return NotFound();
        }

        unitOfWork.BeginTransaction();

        try
        {
            await unitOfWork.Users.Delete(id);
            unitOfWork.CommitTransaction();
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            unitOfWork.RollbackTransaction();
            return Problem(ex.Message);
        }
    }

    private bool UserExists(int id)
    {
        return unitOfWork.Users.Find(id) != null;
    }
}
