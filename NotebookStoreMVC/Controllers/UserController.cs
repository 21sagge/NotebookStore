using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NotebookStore.Business;
using NotebookStoreMVC.Models;
using Microsoft.AspNetCore.Authorization;

namespace NotebookStoreMVC.Controllers;

public class UserController : Controller
{
    private readonly IServices services;
    private readonly IMapper mapper;

    public UserController(IServices services, IMapper mapper)
    {
        this.services = services;
        this.mapper = mapper;
    }

    // GET: userViewModel
    [HttpGet]
    // [Authorize]
    public async Task<IActionResult> Index()
    {
        var users = await services.Users.GetAll();
        var mappedUsers = mapper.Map<IEnumerable<UserViewModel>>(users);

        return View(mappedUsers);
    }

    // GET: userViewModel/Details/5
    [HttpGet]
    // [Authorize]
    public async Task<IActionResult> Details(int id)
    {
        var user = await services.Users.Find(id);

        if (user == null)
        {
            return NotFound();
        }

        return View(mapper.Map<UserViewModel>(user));
    }

    // GET: userViewModel/Create
    [HttpGet]
    [Authorize]
    public IActionResult Create()
    {
        return View();
    }

    // POST: userViewModel/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,Email,Password,Role,Token")] UserViewModel userViewModel)
    {
        if (ModelState.IsValid)
        {
            await services.Users.Create(mapper.Map<UserDto>(userViewModel));

            return RedirectToAction(nameof(Index));
        }

        return View(userViewModel);
    }

    // GET: userViewModel/Edit/5
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Edit(int id)
    {
        var user = await services.Users.Find(id);

        if (user == null)
        {
            return NotFound();
        }

        return View(mapper.Map<UserViewModel>(user));
    }

    // POST: userViewModel/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,Password,Role,Token")] UserViewModel userViewModel)
    {
        if (id != userViewModel.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            await services.Users.Update(mapper.Map<UserDto>(userViewModel));

            return RedirectToAction(nameof(Index));
        }

        return View(userViewModel);
    }

    // GET: userViewModel/Delete/5
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Delete(int id)
    {
        var user = await services.Users.Find(id);

        if (user == null)
        {
            return NotFound();
        }

        return View(mapper.Map<UserViewModel>(user));
    }

    // POST: userViewModel/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await services.Users.Delete(id);

        return RedirectToAction(nameof(Index));
    }
}
