using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NotebookStore.Business.Mapping;
using NotebookStore.DAL;

namespace NotebookStore.Business.Tests;

public class TestStartup
{
    private static ServiceCollection services;

    static TestStartup()
    {
        services = new ServiceCollection();

        services.AddDbContext<NotebookStoreContext.NotebookStoreContext>(options =>
        {
            options.UseSqlite("DataSource=notebookStoreTest.db");
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddSingleton(new MapperConfiguration(cfg => cfg.AddProfile<BusinessMapper>()).CreateMapper());

        
    }

    public ServiceProvider GetProvider()
    {
        return services.BuildServiceProvider();
    }

    public void Register<T>() where T : class
    {
        services.AddSingleton<T>();
    }

    public T Resolve<T>(ServiceProvider serviceProvider)
    {
        return serviceProvider.GetRequiredService<T>();
    }

    public void Register<T>(T mock) where T : class
    {
        services.AddSingleton<T>(mock);
    }
}
