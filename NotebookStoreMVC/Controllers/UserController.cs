using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NotebookStore.Business;
using NotebookStoreMVC.Models;
using Microsoft.AspNetCore.Authorization;

namespace NotebookStoreMVC.Controllers;

[Authorize]
public class UserController : Controller
{
	private readonly IUserService services;
	private readonly IMapper mapper;

	public UserController(IUserService services, IMapper mapper)
	{
		this.services = services;
		this.mapper = mapper;
	}

	// GET: userViewModel
	[HttpGet]
	public async Task<IActionResult> Index()
	{
		var users = await services.GetUsers();

		foreach (var user in users)
		{
			var roles = await services.GetUserRoles(user.Id);
			user.Role = roles?.LastOrDefault() ?? string.Empty;
			// user.Roles = roles?.ToArray() ?? new string[0];
		}

		return View(mapper.Map<IEnumerable<UserViewModel>>(users));
	}

	// GET: userViewModel/Edit/5
	[HttpGet]
	[Authorize(Roles = "Admin")]
	public async Task<IActionResult> Edit(string id)
	{
		var user = await services.GetUser(id);

		if (user == null)
		{
			return NotFound();
		}

		var roles = await services.GetUserRoles(user.Id);
		user.Role = roles?.LastOrDefault() ?? string.Empty;
		// user.Roles = roles?.ToArray() ?? new string[0];

		return View(mapper.Map<UserViewModel>(user));
	}

	// POST: userViewModel/Edit/5
	[HttpPost]
	[ValidateAntiForgeryToken]
	[Authorize(Roles = "Admin")]
	public async Task<IActionResult> Edit(string id, [Bind("Id,Role")] UserViewModel userViewModel)
	{
		if (id != userViewModel.Id)
		{
			return NotFound();
		}

		if (ModelState.IsValid)
		{
			var roles = await services.GetUserRoles(userViewModel.Id);

			var role = roles?.LastOrDefault() ?? string.Empty;

			if (role != null && !string.IsNullOrEmpty(userViewModel.Role))
			{
				var result = await services.AddUserToRole(userViewModel.Id, userViewModel.Role);
				if (result)
				{
					await services.RemoveUserFromRole(userViewModel.Id, role);
				}
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
		var user = await services.GetUser(id);

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
		if (User.IsInRole("Admin") && user.Role == "Admin")
		{
			return RedirectToAction(nameof(Index));
		}

		var roles = await services.GetUserRoles(user.Id);
		user.Role = roles?.LastOrDefault() ?? string.Empty;

		return View(mapper.Map<UserViewModel>(user));
	}

	// POST: userViewModel/Delete/5
	[HttpPost, ActionName("Delete")]
	[ValidateAntiForgeryToken]
	[Authorize(Roles = "Admin")]
	public async Task<IActionResult> DeleteConfirmed(string id)
	{
		await services.DeleteUser(id);

		return RedirectToAction(nameof(Index));
	}
}
