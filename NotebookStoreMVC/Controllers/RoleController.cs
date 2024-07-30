using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NotebookStore.Business;
using NotebookStore.Business.Context;
using NotebookStoreMVC.Models;

namespace NotebookStoreMVC.Controllers;

[Authorize(Roles = "Admin")]
public class RoleController : Controller
{
	private readonly IRoleService roleService;
	private readonly IMapper mapper;
    private readonly IUserService userService;
    private readonly SignInManager<IdentityUser> signInManager;

    public RoleController(IRoleService roleService, IMapper mapper, IUserService userService, SignInManager<IdentityUser> signInManager)
	{
		this.roleService = roleService;
		this.mapper = mapper;
        this.userService = userService;
        this.signInManager = signInManager;
    }

	// GET: RoleViewModel
	[HttpGet]
	public async Task<IActionResult> Index()
	{
		var roles = await roleService.GetRoles();

		return View(mapper.Map<IEnumerable<RoleViewModel>>(roles));
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

		var roleViewModel = mapper.Map<RoleViewModel>(role);

		var claims = await roleService.GetClaims(role.Name);

		foreach (var claim in claims)
		{
			roleViewModel.Claims.Add(claim);
		}

		ViewBag.Claims = Claims.AllClaims;

		return View(roleViewModel);
	}

	// POST: RoleViewModel/Edit/RoleName
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Edit(string name, [Bind("Id,Name,Claims")] RoleViewModel roleViewModel)
	{
		if (name != roleViewModel.Name)
		{
			return NotFound();
		}

		if (ModelState.IsValid)
		{
			var result = await roleService.UpdateRole(roleViewModel.Name, roleViewModel.Claims);

			if (!result)
			{
				ModelState.AddModelError(string.Empty, "Role not found.");
			}
			else
			{
				var currentUser = await userService.GetCurrentUser();

				await signInManager.RefreshSignInAsync(mapper.Map<IdentityUser>(currentUser));

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
