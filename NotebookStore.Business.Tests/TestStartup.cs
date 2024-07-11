using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NotebookStore.Business.Context;
using NotebookStore.DAL;

namespace NotebookStore.Business.Tests;

public class TestStartup
{
    private static readonly ServiceCollection services;

    // Register services
    static TestStartup()
    {
        services = new ServiceCollection();

        services.RegisterNotebookBusiness();

        services.AddDbContext<NotebookStoreContext.NotebookStoreContext>(options =>
        {
            options.UseSqlite($"DataSource=notebookStoreTest_{Guid.NewGuid()}.db");
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        services.AddScoped<IUserContext, ConsoleUserContext>();

        services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<NotebookStoreContext.NotebookStoreContext>();
    }

    /// <summary>
    /// Register a service
    /// </summary>
    /// <typeparam name="T">Service type</typeparam>
    public static void Register<T>() where T : class
    => services.AddSingleton<T>();

    /// <summary>
    /// Register a mock service
    /// </summary>
    /// <typeparam name="T">Service type</typeparam>
    public static void Register<T>(T mock) where T : class
    => services.AddSingleton(mock);


    /// <summary>
    /// Create a ComponentsContext
    /// </summary>
    /// <returns>ComponentsContext</returns>
    public static ComponentsContext CreateComponentsContext()
    {
        var serviceProvider = services.BuildServiceProvider();

        var context = serviceProvider.GetRequiredService<NotebookStoreContext.NotebookStoreContext>();

        context.Database.EnsureCreated();
        // context.Database.BeginTransaction();

        return new ComponentsContext(serviceProvider);
    }

    public class ComponentsContext : IDisposable
    {
        private readonly ServiceProvider serviceProvider;

        private readonly Lazy<NotebookStoreContext.NotebookStoreContext> context;

        public NotebookStoreContext.NotebookStoreContext DbContext
        => context.Value;

        public ComponentsContext(ServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;

            context = new Lazy<NotebookStoreContext.NotebookStoreContext>(serviceProvider.GetRequiredService<NotebookStoreContext.NotebookStoreContext>, true);
        }

        /// <summary>
        /// Resolve a service
        /// </summary>
        /// <typeparam name="T">Service type</typeparam>
        public T Resolve<T>() where T : class
        => serviceProvider.GetRequiredService<T>();

        /// <summary>
        /// Dispose the context
        /// </summary>
        public void Dispose()
        {
            context.Value.Database.EnsureDeleted();
            context.Value.Dispose();
            serviceProvider.Dispose();
        }
    }
}
