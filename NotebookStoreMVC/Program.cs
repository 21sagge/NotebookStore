using Microsoft.EntityFrameworkCore;
using NotebookStoreMVC;
using NotebookStore.DAL;
using NotebookStoreMVC.Services;
using NotebookStore.Business;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<BrandService>();
builder.Services.AddScoped<CpuService>();
builder.Services.AddScoped<DisplayService>();
builder.Services.AddScoped<MemoryService>();
builder.Services.AddScoped<ModelService>();
builder.Services.AddScoped<NotebookService>();
builder.Services.AddScoped<StorageService>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddAutoMapper(configure =>
{
    configure.AddProfile(new MapperMvc());
});

//builder.Services.AddKeyedScoped<ISerializer, JsonHandler>("json");
//builder.Services.AddKeyedScoped<ISerializer, XmlHandler>("xml");
builder.Services.AddScoped<ISerializer, JsonHandler>();
builder.Services.AddScoped<ISerializer, XmlHandler>();

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
