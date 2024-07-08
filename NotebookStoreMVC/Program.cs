using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NotebookStoreMVC;
using NotebookStore.DAL;
using NotebookStore.Business;
using NotebookStore.Business.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IUserContext, HttpUserContext>();

builder.Services.AddScoped<IServices, Services>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<ISerializer, JsonHandler>();
builder.Services.AddScoped<ISerializer, XmlHandler>();

builder.Services.AddAutoMapper(configure =>
{
    configure.AddProfile(new MapperMvc());
});

builder.Services.RegisterNotebookBusiness();

builder.Services.AddDbContext<NotebookStoreContext.NotebookStoreContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("SqlLite"),
        b => b.MigrationsAssembly("NotebookStoreContext"));
    options.EnableDetailedErrors();
});

// Default Identity
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<NotebookStoreContext.NotebookStoreContext>();

builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings.
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;

    // Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // User settings.
    options.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = false;
});

builder.Services.ConfigureApplicationCookie(options =>
{
    // Cookie settings
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromHours(4);

    options.LoginPath = "/Identity/Account/Login";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
    options.SlidingExpiration = true;
});

// builder.Services.AddAuthorization(options =>
// {
//     options.AddPolicy("Admin", policy => policy.RequireClaim("Role", "Admin"));
//     options.AddPolicy("Editor", policy => policy.RequireClaim("Role", "Editor"));
//     options.AddPolicy("User", policy => policy.RequireClaim("Role", "User"));
//     options.AddPolicy("All", policy => policy.RequireClaim("Role", "Admin", "Editor", "User"));
// });

builder.Logging.AddConsole();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
