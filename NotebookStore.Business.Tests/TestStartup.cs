using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NotebookStore.Business.Mapping;
using NotebookStore.DAL;

namespace NotebookStore.Business.Tests;

public class TestStartup
{
    private static readonly ServiceCollection services;

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

    /// <summary>
    /// Register a service
    /// </summary>
    /// <typeparam name="T">Service type</typeparam>
    public void Register<T>() where T : class
    => services.AddSingleton<T>();

    /// <summary>
    /// Register a mock service
    /// </summary>
    /// <typeparam name="T">Service type</typeparam>
    public void Register<T>(T mock) where T : class
    => services.AddSingleton<T>(mock);

    /// <summary>
    /// Resolve a service
    /// </summary>
    /// <typeparam name="T">Service type</typeparam>
    public T Resolve<T>(ServiceProvider serviceProvider) where T : class
    => serviceProvider.GetRequiredService<T>();

    /// <summary>
    /// Get service provider
    /// </summary>
    /// <returns>Service provider</returns>
    public ServiceProvider GetProvider()
    => services.BuildServiceProvider();
}
