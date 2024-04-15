using Microsoft.EntityFrameworkCore;
using NotebookStoreMVC;
using NotebookStoreMVC.Models;
using NotebookStoreMVC.Repositories;
using NotebookStoreMVC.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IRepository<BrandViewModel>, BrandRepository>();
builder.Services.AddScoped<IRepository<CpuViewModel>, CpuRepository>();
builder.Services.AddScoped<IRepository<DisplayViewModel>, DisplayRepository>();
builder.Services.AddScoped<IRepository<MemoryViewModel>, MemoryRepository>();
builder.Services.AddScoped<IRepository<ModelViewModel>, ModelRepository>();
builder.Services.AddScoped<IRepository<StorageViewModel>, StorageRepository>();
builder.Services.AddScoped<INotebookRepository, NotebookRepository>();

builder.Services.AddAutoMapper(configure =>
{
    configure.AddProfile(new MapperMvc());
});

builder.Services.AddKeyedScoped<ISerializer, JsonHandler>("json");
builder.Services.AddKeyedScoped<ISerializer, XmlHandler>("xml");

builder.Services.AddDbContext<NotebookStoreContext.NotebookStoreContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("SqlLite")));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
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
