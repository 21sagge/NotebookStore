using Microsoft.Extensions.DependencyInjection;
using NotebookStore.Business;

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

		services.AddSingleton<IJsonFileParser, JsonFileParser>();

		return services.BuildServiceProvider();
	}
}
