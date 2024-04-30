using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NotebookStore.Business;
using NotebookStore.DAL;

namespace NotebookStoreTestConsole;

class Program
{
    static void Main(string[] args)
    {
        var serviceProvider = new ServiceCollection()
            .AddDbContext<NotebookStoreContext.NotebookStoreContext>(options => options.UseSqlite("Data Source=NotebookStoreMVC/notebookstore.db"))
            .AddAutoMapper(configure =>
            {
                configure.AddProfile(new MapperMvc());
            })
            .AddScoped<IUnitOfWork, UnitOfWork>()
            .AddScoped<NotebookService>()
            .BuildServiceProvider();

        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<NotebookStoreContext.NotebookStoreContext>();
        var service = scope.ServiceProvider.GetRequiredService<NotebookService>();

        var notebooks = service.GetAll();

        foreach (var notebook in notebooks.Result)
        {
            Console.WriteLine($"{notebook.Brand?.Name} {notebook.Model?.Name} {notebook.Cpu?.Model}");
        }
    }
}