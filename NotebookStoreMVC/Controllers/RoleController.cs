﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotebookStore.Business;
using NotebookStoreMVC.Models;

namespace NotebookStoreMVC.Controllers;

[Authorize(Roles = "Admin")]
public class RoleController : Controller
{
	private readonly IRoleService roleService;

	public RoleController(IRoleService roleService)
	{
		this.roleService = roleService;
	}

	// GET: RoleViewModel
	[HttpGet]
	public async Task<IActionResult> Index()
	{
		var roles = await roleService.GetRoles();

		var roleViewModels = new List<RoleViewModel>();

		foreach (var role in roles)
		{
			if (role == null)
			{
				continue;
			}

			roleViewModels.Add(new RoleViewModel
			{
				Name = role,
			});
		}

		return View(roleViewModels);
	}

	// GET: RoleViewModel/Create
	[HttpGet]
	public IActionResult Create()
	{
		return View();
	}

	// POST: RoleViewModel/Create
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Create([Bind("Name")] RoleViewModel roleViewModel)
	{
		if (ModelState.IsValid)
		{
			var result = await roleService.CreateRole(roleViewModel.Name);

			if (!result)
			{
				ModelState.AddModelError(string.Empty, "Role already exists.");
			}
			else
			{
				return RedirectToAction(nameof(Index));
			}
		}
		return View(roleViewModel);
	}

	// GET: RoleViewModel/Edit/RoleName
	[HttpGet]
	public async Task<IActionResult> Edit(string name)
	{
		var role = await roleService.GetRole(name);

		if (role == null)
		{
			return NotFound();
		}

		var roleViewModel = new RoleViewModel
		{
			Name = role,
			Claims = new List<string>(),
			AllClaims = new List<string>()
		};

		var claims = await roleService.GetClaims(role);

		foreach (var claim in claims)
		{
			roleViewModel.Claims.Add(claim);
		}

		var allClaims = await roleService.GetAllClaims();

		foreach (var claim in allClaims)
		{
			roleViewModel.AllClaims.Add(claim);
		}

		return View(roleViewModel);
	}

	// POST: RoleViewModel/Edit/RoleName
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Edit(string name, [Bind("Name,Claims")] RoleViewModel roleViewModel)
	{
		if (name != roleViewModel.Name)
		{
			return NotFound();
		}

		if (ModelState.IsValid)
		{
			roleViewModel.Claims ??= new List<string>();

			var result = await roleService.UpdateRole(roleViewModel.Name, roleViewModel.Claims);

			if (!result)
			{
				ModelState.AddModelError(string.Empty, "Role not found.");
			}
			else
			{
				return RedirectToAction(nameof(Index));
			}
		}

		return View(roleViewModel);
	}

	// POST: RoleViewModel/Delete/RoleName
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Delete(string name)
	{
		await roleService.DeleteRole(name);

		return RedirectToAction(nameof(Index));
	}
}
