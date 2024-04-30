using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using NotebookStore.Business;
using NotebookStoreMVC.Models;

namespace NotebookStoreMVC;

public class AccountController : Controller
{
	private readonly UserService service;
	private readonly IMapper mapper;

	public AccountController(UserService service, IMapper mapper)
	{
		this.service = service;
		this.mapper = mapper;
	}

	[HttpGet]
	public IActionResult Login()
	{
		return View();
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> LoginAsync(UserViewModel user)
	{
		if (ModelState.IsValid)
		{
			// Check if user exists in the database
			var loggedInUser = await service.Find(user.Email, user.Password);

			if (loggedInUser != null && loggedInUser.Email == user.Email && loggedInUser.Password == user.Password)
			{
				// Create a new ClaimsIdentity
				var claims = new List<Claim>
				{
					new Claim(ClaimTypes.Name, user.Name),
					new Claim(ClaimTypes.Email, user.Email),
				};
				var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

				// Create a new ClaimsPrincipal
				var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

				// Sign in the user
				await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

				return RedirectToAction("Index", "Home");
			}
			else
			{
				ModelState.AddModelError(string.Empty, "Invalid login attempt.");
			}
		}

		return View(user);
	}

	[HttpGet]
	public IActionResult Logout()
	{
		return View();
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> LogoutAsync()
	{
		await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

		return RedirectToAction("Index", "Home");
	}

	[HttpGet]
	public IActionResult AccessDenied()
	{
		return View();
	}

	[HttpGet]
	public IActionResult Register()
	{
		return View();
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Register(UserViewModel user)
	{
		if (ModelState.IsValid)
		{
			await service.Create(mapper.Map<UserDto>(user));

			return RedirectToAction("Login");
		}

		return View(user);
	}
}
