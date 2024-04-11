using Microsoft.EntityFrameworkCore;
using NotebookStore.Entities;
using NotebookStoreMVC;
using NotebookStoreMVC.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IRepository<Brand>, BrandRepository>();
builder.Services.AddScoped<IRepository<Cpu>, CpuRepository>();
builder.Services.AddScoped<IRepository<Display>, DisplayRepository>();
builder.Services.AddScoped<IRepository<Memory>, MemoryRepository>();
builder.Services.AddScoped<IRepository<Model>, ModelRepository>();
builder.Services.AddScoped<IRepository<Storage>, StorageRepository>();
builder.Services.AddScoped<INotebookRepository, NotebookRepository>();

builder.Services.AddDbContext<NotebookStoreContext.NotebookStoreContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("SqlLite")));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

if (app.Environment.IsDevelopment())
{
    app.UseDeveloper("Rosario").UseDeveloper("Niccolo");
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

