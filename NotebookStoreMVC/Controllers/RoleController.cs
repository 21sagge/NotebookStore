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
	public async Task<IActionResult> Create([Bind("Id,Name,Claims")] RoleViewModel roleViewModel)
	{
		if (ModelState.IsValid)
		{
			await roleService.CreateRole(roleViewModel.Name);

			return RedirectToAction(nameof(Index));
		}
		else
		{
			foreach (var modelState in ModelState.Values)
			{
				foreach (var error in modelState.Errors)
				{
					Console.WriteLine(error.ErrorMessage);
				}
			}
		}

		return View(roleViewModel);
	}

	// GET: RoleViewModel/Edit/Id
	[HttpGet]
	public async Task<IActionResult> Edit(string id)
	{
		var role = await roleService.GetRole(id);

		if (role == null)
		{
			return NotFound();
		}

		ViewBag.Claims = Claims.AllClaims;

		return View(mapper.Map<RoleViewModel>(role));
	}

	// POST: RoleViewModel/Edit/Id
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Edit(string id, [Bind("Id,Name,Claims")] RoleViewModel roleViewModel)
	{
		if (id != roleViewModel.Id)
		{
			return NotFound();
		}

		if (ModelState.IsValid)
		{
			var result = await roleService.UpdateRole(mapper.Map<RoleDto>(roleViewModel));

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

	// POST: RoleViewModel/Delete/Id
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Delete(string id)
	{
		await roleService.DeleteRole(id);

		return RedirectToAction(nameof(Index));
	}
}
