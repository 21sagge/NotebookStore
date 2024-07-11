using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NotebookStore.Business.Context;
using NotebookStore.DAL;

namespace NotebookStore.Business.Tests;

/// <summary>
/// Represents the startup class for testing purposes.
/// </summary>
public class TestStartup
{
    private static readonly ServiceCollection services;

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

        var userServiceMock = new Mock<IUserService>();
        var permissionServiceMock = new Mock<IPermissionService>();

        services.AddSingleton(userServiceMock.Object);
        services.AddSingleton(permissionServiceMock.Object);
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

        return new ComponentsContext(serviceProvider, context);
    }

    /// <summary>
    /// Represents a context that provides access to various components and services used in the application.
    /// </summary>
    public class ComponentsContext : IDisposable
    {
        private readonly ServiceProvider serviceProvider;

        private readonly Lazy<NotebookStoreContext.NotebookStoreContext> context;

        private readonly Lazy<IUserService> userService;
        private readonly Lazy<IPermissionService> permissionService;

        /// <summary>
        /// Gets the instance of the NotebookStoreContext.NotebookStoreContext used by the context.
        /// </summary>
        public NotebookStoreContext.NotebookStoreContext DbContext => context.Value;

        /// <summary>
        /// Gets the instance of the IUserService used by the context.
        /// </summary>
        public IUserService UserService => userService.Value;

        /// <summary>
        /// Gets the instance of the IPermissionService used by the context.
        /// </summary>
        public IPermissionService PermissionService => permissionService.Value;

        /// <summary>
        /// Initializes a new instance of the ComponentsContext class.
        /// </summary>
        /// <param name="serviceProvider">The service provider used to resolve services.</param>
        /// <param name="context">The instance of the NotebookStoreContext.NotebookStoreContext used by the context.</param>
        public ComponentsContext(ServiceProvider serviceProvider, NotebookStoreContext.NotebookStoreContext context)
        {
            this.serviceProvider = serviceProvider;
            this.context = new Lazy<NotebookStoreContext.NotebookStoreContext>(() => context, isThreadSafe: true);

            userService = new Lazy<IUserService>(
                serviceProvider.GetRequiredService<IUserService>,
                isThreadSafe: true
            );

            permissionService = new Lazy<IPermissionService>(
                serviceProvider.GetRequiredService<IPermissionService>,
                isThreadSafe: true
            );
        }

        /// <summary>
        /// Resolve a service from the context.
        /// </summary>
        /// <typeparam name="T">The type of the service to resolve.</typeparam>
        /// <returns>The resolved service instance.</returns>
        public T Resolve<T>() where T : class
        {
            return serviceProvider.GetRequiredService<T>();
        }

        /// <summary>
        /// Dispose the context and release any resources used.
        /// </summary>
        public void Dispose()
        {
            context.Value.Database.EnsureDeleted();
            context.Value.Dispose();
            serviceProvider.Dispose();
        }
    }
}
