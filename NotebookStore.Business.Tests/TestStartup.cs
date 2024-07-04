using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NotebookStore.Business.Mapping;
using NotebookStore.DAL;

namespace NotebookStore.Business.Tests;

public static class TestStartup
{
	public static ServiceProvider InitializeIoC()
	{
		var services = new ServiceCollection();

		services.AddDbContext<NotebookStoreContext.NotebookStoreContext>(options =>
		{
			options.UseSqlite("DataSource=notebookStoreTest.db");
		});

		services.AddScoped<IUnitOfWork, UnitOfWork>();

		services.AddSingleton(new MapperConfiguration(cfg => cfg.AddProfile<BusinessMapper>()).CreateMapper());

		services.AddSingleton(new Mock<IUserService>());
		services.AddSingleton(provider => provider.GetRequiredService<Mock<IUserService>>().Object);

		services.AddSingleton(new Mock<IPermissionService>());
		services.AddSingleton(provider => provider.GetRequiredService<Mock<IPermissionService>>().Object);

		return services.BuildServiceProvider();
	}
}
