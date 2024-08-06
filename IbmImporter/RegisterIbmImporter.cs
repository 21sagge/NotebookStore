using IbmImporter.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NotebookStore.Business;
using NotebookStore.DAL;
using Monitor = IbmImporter.Models.Monitor;

namespace IbmImporter;

public static class RegisterIbmImporter
{
	/// <summary>
	/// Registers the services for the IBM importer
	/// </summary>
	/// <param name="services"> Service collection </param>
	/// <returns> Service provider </returns>
	public static ServiceProvider Register()
	{
		var services = new ServiceCollection();

		services.RegisterNotebookBusiness();

		services.AddDbContext<NotebookStoreContext.NotebookStoreContext>(options =>
		{
			options.UseSqlite($"DataSource=../NotebookStoreMVC/notebookstore.db");
		});

		services.AddScoped<IJsonFileParser, JsonFileParser>();

		services.AddScoped<IValidator<NotebookData>, NotebookDataValidator>();
		services.AddScoped<IValidator<Notebook>, NotebookValidator>();
		services.AddScoped<IValidator<Monitor>, MonitorValidator>();
		services.AddScoped<IValidator<Ports>, PortsValidator>();

		services.AddScoped<DataImporter>();

		services.AddScoped<IRepository<NotebookStore.Entities.Notebook>, NotebookRepository>();

		return services.BuildServiceProvider();
	}
}
