using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using NotebookStore.Business;
using NotebookStore.DAL;
using NotebookStoreMVC;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using NotebookStore.Business.Context;

namespace NotebookStoreTestConsole;

class Program
{
    static void Main(string[] args)
    {
        var builder = new ConfigurationBuilder();
        builder.SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        IConfiguration config = builder.Build();

        var services = new ServiceCollection();

        services.AddDefaultIdentity<IdentityUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<NotebookStoreContext.NotebookStoreContext>();

        services.AddSingleton(config)
            .AddSingleton<IUserContext, ConsoleUserContext>()
            .AddDbContext<NotebookStoreContext.NotebookStoreContext>(option =>
            {
                option.UseLazyLoadingProxies();
                option.UseSqlite(config.GetSection("ConnectionStrings").GetSection("SqlLite").Value, b =>
                {
                    b.MigrationsAssembly("NotebookStoreContext");
                });
            })
            .AddAutoMapper(configure =>
            {
                configure.AddProfile(new MapperMvc());
            })
            .AddScoped<IUnitOfWork, UnitOfWork>()
            .AddScoped<IServices, Services>();

        var serviceProvider = services.BuildServiceProvider();

        var service = serviceProvider.GetRequiredService<IServices>();

        //FetchAndPrintAsync(service);
    }

    private static async void FetchAndPrintAsync(IServices service)
    {
        var currentUser = await service.Users.GetCurrentUser();

        var notebooks = await service.Notebooks.GetAll();

        Console.WriteLine("All notebooks:");

        foreach (var notebook in notebooks)
        {
            Console.WriteLine($"{notebook.Brand?.Name} {notebook.Model?.Name} {notebook.Cpu?.Brand} {notebook.Cpu?.Model} {notebook.Memory?.Capacity}Gb");
        }

        Console.WriteLine();

        Console.WriteLine("Notebooks with more than 16GB memory:");

        var notebooksWithMoreThan16GBMemory = notebooks.Where(n => n.Memory?.Capacity > 16);

        foreach (var notebook in notebooksWithMoreThan16GBMemory)
        {
            Console.WriteLine($"{notebook.Brand?.Name} {notebook.Model?.Name} {notebook.Memory?.Capacity}Gb");
        }

        Console.ReadKey();
    }
}
