using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NotebookStore.Business.Context;
using NotebookStore.Business.Mapping;
using NotebookStore.DAL;

namespace NotebookStore.Business
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterNotebookBusiness(this IServiceCollection services)
        {
            services.AddAutoMapper(configure =>
            {
                configure.AddProfile(new BusinessMapper());
            });

            services.AddScoped<IUserContext, ConsoleUserContext>();

            services.AddScoped<IUserContext, ConsoleUserContext>();
            services.AddScoped<UserManager<IdentityUser>>();
            services.AddScoped<IUserStore<IdentityUser>, UserStore<IdentityUser, IdentityRole, NotebookStoreContext.NotebookStoreContext, string>>();
            services.AddScoped<IPasswordHasher<IdentityUser>, PasswordHasher<IdentityUser>>();
            services.AddScoped<ILookupNormalizer, UpperInvariantLookupNormalizer>();
            services.AddScoped<IdentityErrorDescriber>();
            services.AddScoped<ILogger<UserManager<IdentityUser>>, Logger<UserManager<IdentityUser>>>();
            services.AddScoped<ILoggerFactory, LoggerFactory>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
