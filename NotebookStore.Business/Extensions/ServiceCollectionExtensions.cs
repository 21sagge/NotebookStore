using Microsoft.Extensions.DependencyInjection;
using NotebookStore.Business.Mapping;

namespace NotebookStore.Business
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers the necessary services for the notebook business.
        /// (BusinessMapper, ISerializer, IServices, IUserService, IRoleService, JsonHandler, XmlHandler)
        /// </summary>
        /// <param name="services">The IServiceCollection to add the services to.</param>
        public static void RegisterNotebookBusiness(this IServiceCollection services)
        {
            services.AddAutoMapper(configure =>
            {
                configure.AddProfile(new BusinessMapper());
            });

            services.AddScoped<ISerializer, JsonHandler>();
            services.AddScoped<ISerializer, XmlHandler>();

            services.AddScoped<IServices, Services>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();

            services.AddScoped<ISerializer, JsonHandler>();
            services.AddScoped<ISerializer, XmlHandler>();
        }
    }
}
