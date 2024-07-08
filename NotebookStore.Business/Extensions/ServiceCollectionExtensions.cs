using Microsoft.Extensions.DependencyInjection;
using NotebookStore.Business.Mapping;

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
        }
    }
}
