using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NotebookStore.Business;
using NotebookStoreMVC.Models;
using Microsoft.AspNetCore.Authorization;

namespace NotebookStoreMVC.Controllers;

[Authorize]
public class UserController : Controller
{
	private readonly IUserService userService;
	private readonly IMapper mapper;
    private readonly IRoleService roleService;

    public UserController(IUserService userService, IMapper mapper, IRoleService roleService)
	{
		this.userService = userService;
		this.mapper = mapper;
        this.roleService = roleService;
    }

	// GET: userViewModel
	[HttpGet]
	public async Task<IActionResult> Index()
	{
		var users = await userService.GetUsers();

		foreach (var user in users)
		{
			var roles = await userService.GetUserRoles(user.Id);
			user.Roles = roles?.ToArray() ?? Array.Empty<string>();
		}

		return View(mapper.Map<IEnumerable<UserViewModel>>(users));
	}

	// GET: userViewModel/Edit/5
	[HttpGet]
	[Authorize(Roles = "Admin")]
	public async Task<IActionResult> Edit(string id)
	{
		var user = await userService.GetUser(id);

		if (user == null)
		{
			return NotFound();
		}

		var roles = await userService.GetUserRoles(user.Id);
		user.Roles = roles?.ToArray() ?? Array.Empty<string>();

		ViewData["Roles"] = await roleService.GetRoles();

		return View(mapper.Map<UserViewModel>(user));
	}

	// POST: userViewModel/Edit/5
	[HttpPost]
	[ValidateAntiForgeryToken]
	[Authorize(Roles = "Admin")]
	public async Task<IActionResult> Edit(string id, [Bind("Id,Roles")] UserViewModel userViewModel)
	{
		if (id != userViewModel.Id)
		{
			return NotFound();
		}

		if (ModelState.IsValid)
		{
			var roles = await userService.GetUserRoles(userViewModel.Id);

			if (roles != null)
			{
				await userService.RemoveUserRoles(userViewModel.Id, roles.ToArray());
			}

			var result = await userService.AddUserRoles(userViewModel.Id, userViewModel.Roles);

			if (!result)
			{
				return BadRequest();
			}

			return RedirectToAction(nameof(Index));
		}
		else
		{
			var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
		}

		return View(userViewModel);
	}

	// GET: userViewModel/Delete/5
	[HttpGet]
	[Authorize(Roles = "Admin")]
	public async Task<IActionResult> Delete(string id)
	{
		var user = await userService.GetUser(id);

		if (user == null)
		{
			return NotFound();
		}

		// if user is the current user, redirect to the index page
		if (User.Identity!.Name == user.Name)
		{
			return RedirectToAction(nameof(Index));
		}

		// if user is an admin, redirect to the index page
		if (User.IsInRole("Admin") && user.Roles.Contains("Admin"))
		{
			return RedirectToAction(nameof(Index));
		}

		var roles = await userService.GetUserRoles(user.Id);
		user.Roles = roles?.ToArray() ?? Array.Empty<string>();

		return View(mapper.Map<UserViewModel>(user));
	}

	// POST: userViewModel/Delete/5
	[HttpPost, ActionName("Delete")]
	[ValidateAntiForgeryToken]
	[Authorize(Roles = "Admin")]
	public async Task<IActionResult> DeleteConfirmed(string id)
	{
		await userService.DeleteUser(id);

		return RedirectToAction(nameof(Index));
	}
}
