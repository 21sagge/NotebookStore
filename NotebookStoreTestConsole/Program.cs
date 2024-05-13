using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using NotebookStore.Business;
using NotebookStore.DAL;
using NotebookStoreMVC;

namespace NotebookStoreTestConsole;

class Program
{
    static void Main(string[] args)
    {
        var builder = new ConfigurationBuilder();
        builder.SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        IConfiguration config = builder.Build();

        var serviceProvider = new ServiceCollection()
            .AddSingleton<IConfiguration>(config)
            .AddDbContext<NotebookStoreContext.NotebookStoreContext>(option =>
            {
                option.UseLazyLoadingProxies();
                option.UseSqlite(config.GetSection("ConnectionStrings").GetSection("SqlLite").Value);
            })
            .AddAutoMapper(configure =>
            {
                configure.AddProfile(new MapperMvc());
            })
            .AddScoped<IUnitOfWork, UnitOfWork>()
            .AddScoped<IServices, Services>()
            .BuildServiceProvider();

        using var scope = serviceProvider.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<IServices>();

        FetchAndPrintAsync(service);
    }

    private static async void FetchAndPrintAsync(IServices service)
    {
        var notebooks = await service.Notebooks.GetAll();

        foreach (var notebook in notebooks)
        {
            Console.WriteLine($"{notebook.Brand?.Name} {notebook.Model?.Name} {notebook.Cpu?.Model}");
        }

        var brand1 = await service.Brands.Find(1);

        Console.WriteLine(brand1.Name);

        var display1 = await service.Displays.Find(1);

        Console.WriteLine($"{display1.Size}\" {display1.ResolutionWidth}x{display1.ResolutionHeight} {display1.PanelType}");

        Console.ReadKey();
    }
}
