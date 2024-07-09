using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
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

        services.AddScoped<BrandService>();
        services.AddScoped<NotebookService>();

        var mockUserService = new Mock<IUserService>();
        var mockPermissionService = new Mock<IPermissionService>();

        services.AddSingleton(mockUserService.Object);
        services.AddSingleton(mockPermissionService.Object);
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
    => services.AddSingleton<T>(mock);

    /// <summary>
    /// Resolve a service
    /// </summary>
    /// <typeparam name="T">Service type</typeparam>
    public static T Resolve<T>() where T : class
    {
        var serviceProvider = services.BuildServiceProvider();

        return serviceProvider.GetRequiredService<T>();
    }

    /// <summary>
    /// Create a ComponentsContext
    /// </summary>
    /// <returns>ComponentsContext</returns>
    public static ComponentsContext CreateComponentsContext()
    {
        var serviceProvider = services.BuildServiceProvider();

        return new ComponentsContext(serviceProvider);
    }

    public class ComponentsContext : IDisposable
    {
        private readonly ServiceProvider serviceProvider;

        private readonly Lazy<NotebookStoreContext.NotebookStoreContext> context;
        private readonly Lazy<IUnitOfWork> unitOfWork;
        private readonly Lazy<Mock<IUserService>> userService;
        private readonly Lazy<Mock<IPermissionService>> permissionService;
        private readonly Lazy<BrandService> brandService;
        private readonly Lazy<NotebookService> notebookService;

        public NotebookStoreContext.NotebookStoreContext DbContext
        => context.Value;
        public IUnitOfWork UnitOfWork
        => unitOfWork.Value;
        public IUserService UserService
        => userService.Value.Object;
        public IPermissionService PermissionService
        => permissionService.Value.Object;
        public BrandService BrandService
        => brandService.Value;
        public NotebookService NotebookService
        => notebookService.Value;

        public ComponentsContext(ServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;

            context = new Lazy<NotebookStoreContext.NotebookStoreContext>(() => serviceProvider.GetRequiredService<NotebookStoreContext.NotebookStoreContext>(), true);
            unitOfWork = new Lazy<IUnitOfWork>(() => serviceProvider.GetRequiredService<IUnitOfWork>(), true);
            brandService = new Lazy<BrandService>(() => serviceProvider.GetRequiredService<BrandService>(), true);
            userService = new Lazy<Mock<IUserService>>(() => serviceProvider.GetRequiredService<Mock<IUserService>>(), true);
            permissionService = new Lazy<Mock<IPermissionService>>(() => serviceProvider.GetRequiredService<Mock<IPermissionService>>(), true);
            notebookService = new Lazy<NotebookService>(() => serviceProvider.GetRequiredService<NotebookService>(), true);
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
