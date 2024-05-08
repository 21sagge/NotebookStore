// using System.Security.Claims;
// using AutoMapper;
// using Microsoft.AspNetCore.Authentication;
// using Microsoft.AspNetCore.Authentication.Cookies;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;
// using NotebookStore.Business;
// using NotebookStoreMVC.Models;

// namespace NotebookStoreMVC;

// public class AccountController : Controller
// {
//     // private readonly UserService service;
//     private readonly IMapper mapper;

//     public AccountController(IMapper mapper)
//     {
//         // this.service = service;
//         this.mapper = mapper;
//     }

//     [HttpGet]
//     [AllowAnonymous]
//     public IActionResult Login()
//     {
//         return View();
//     }

//     [HttpPost]
//     [ValidateAntiForgeryToken]
//     [AllowAnonymous]
//     public async Task<IActionResult> LoginAsync(UserViewModel user)
//     {
//         if (ModelState.IsValid)
//         {
//             // Check if user exists in the database
//             var loggedInUser = await service.Find(user.Email, user.Password);

//             if (loggedInUser != null && loggedInUser.Email == user.Email && loggedInUser.Password == user.Password)
//             {
//                 // Ensure user.Name and user.Email are not null
//                 if (string.IsNullOrEmpty(user.Name) || string.IsNullOrEmpty(user.Email))
//                 {
//                     ModelState.AddModelError(string.Empty, "User name or email is null.");
//                     return View(user);
//                 }

//                 var claims = new List<Claim>
//                 {
//                     new Claim(ClaimTypes.Name, user.Name),
//                     new Claim(ClaimTypes.Email, user.Email)
//                 };

//                 var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

//                 var authProperties = new AuthenticationProperties
//                 {
//                     IsPersistent = false
//                 };

//                 await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

//                 // Sign in the user
//                 // await signInManager.SignInAsync(appUser, isPersistent: false);

//                 return RedirectToAction("Index", "Home");
//             }
//             else
//             {
//                 ModelState.AddModelError(string.Empty, "Invalid login attempt.");
//             }
//         }

//         return View(user);
//     }

//     [HttpGet]
//     // [Authorize]
//     public IActionResult Logout()
//     {
//         return View();
//     }

//     [HttpPost]
//     [ValidateAntiForgeryToken]
//     [Authorize]
//     public async Task<IActionResult> LogoutAsync()
//     {
//         await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

//         return RedirectToAction("Index", "Home");
//     }

//     [HttpGet]
//     public IActionResult AccessDenied()
//     {
//         return View();
//     }

//     [HttpGet]
//     public IActionResult Register()
//     {
//         return View();
//     }

//     [HttpPost]
//     [ValidateAntiForgeryToken]
//     public async Task<IActionResult> Register(UserViewModel user)
//     {
//         if (ModelState.IsValid)
//         {
//             await service.Create(mapper.Map<UserDto>(user));

//             return RedirectToAction("Login");
//         }

//         return View(user);
//     }
// }
