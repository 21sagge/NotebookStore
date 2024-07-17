using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NotebookStoreMVC;
using NotebookStore.DAL;
using NotebookStore.Business;
using NotebookStore.Business.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterNotebookBusiness();

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IUserContext, HttpUserContext>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddAutoMapper(configure =>
{
    configure.AddProfile(new MapperMvc());
});

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

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Edit Brand", policy => policy.RequireClaim("Permission", "Edit Brand"));
    options.AddPolicy("Add Brand", policy => policy.RequireClaim("Permission", "Add Brand"));
    options.AddPolicy("Delete Brand", policy => policy.RequireClaim("Permission", "Delete Brand"));
    options.AddPolicy("Add Notebook", policy => policy.RequireClaim("Permission", "Add Notebook"));
    options.AddPolicy("Edit Notebook", policy => policy.RequireClaim("Permission", "Edit Notebook"));
    options.AddPolicy("Delete Notebook", policy => policy.RequireClaim("Permission", "Delete Notebook"));
    options.AddPolicy("Add Cpu", policy => policy.RequireClaim("Permission", "Add Cpu"));
    options.AddPolicy("Edit Cpu", policy => policy.RequireClaim("Permission", "Edit Cpu"));
    options.AddPolicy("Delete Cpu", policy => policy.RequireClaim("Permission", "Delete Cpu"));
    options.AddPolicy("Add Display", policy => policy.RequireClaim("Permission", "Add Display"));
    options.AddPolicy("Edit Display", policy => policy.RequireClaim("Permission", "Edit Display"));
    options.AddPolicy("Delete Display", policy => policy.RequireClaim("Permission", "Delete Display"));
    options.AddPolicy("Add Memory", policy => policy.RequireClaim("Permission", "Add Memory"));
    options.AddPolicy("Edit Memory", policy => policy.RequireClaim("Permission", "Edit Memory"));
    options.AddPolicy("Delete Memory", policy => policy.RequireClaim("Permission", "Delete Memory"));
    options.AddPolicy("Add Model", policy => policy.RequireClaim("Permission", "Add Model"));
    options.AddPolicy("Edit Model", policy => policy.RequireClaim("Permission", "Edit Model"));
    options.AddPolicy("Delete Model", policy => policy.RequireClaim("Permission", "Delete Model"));
    options.AddPolicy("Add Storage", policy => policy.RequireClaim("Permission", "Add Storage"));
    options.AddPolicy("Edit Storage", policy => policy.RequireClaim("Permission", "Edit Storage"));
    options.AddPolicy("Delete Storage", policy => policy.RequireClaim("Permission", "Delete Storage"));
});

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
