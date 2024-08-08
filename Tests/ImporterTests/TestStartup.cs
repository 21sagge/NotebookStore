﻿using IbmImporter;
using IbmImporter.Models;
using Microsoft.Extensions.DependencyInjection;
using NotebookStore.DAL;
using NotebookStore.Business;
using Microsoft.EntityFrameworkCore;

namespace ImporterTests;

public class TestStartup
{
	private static readonly ServiceCollection services;

	static TestStartup()
	{
		services = new ServiceCollection();

		services.RegisterNotebookBusiness();

		services.AddDbContext<NotebookStoreContext.NotebookStoreContext>(options =>
		{
			options.UseSqlite($"DataSource=ImporterTests_{Guid.NewGuid()}.db");
		});

		services.AddScoped<IValidator<Notebook>, NotebookValidator>();
		services.AddScoped<IValidator<IbmImporter.Models.Monitor>, MonitorValidator>();
		services.AddScoped<IValidator<Ports>, PortsValidator>();

		services.AddScoped<DataImporter>();

		services.AddScoped<IRepository<NotebookStore.Entities.Notebook>, NotebookRepository>();
	}

	public static ComponentsContext CreateComponentsContext()
	{
		services.AddScoped<IJsonFileParser, JsonFileParser>();

		var serviceProvider = services.BuildServiceProvider();

		var context = serviceProvider.GetRequiredService<NotebookStoreContext.NotebookStoreContext>();

		context.Database.EnsureCreated();

		return new ComponentsContext(serviceProvider, context);
	}

	public static ComponentsContext CreateComponentsContext(Action<ServiceCollection> configureServices)
	{
        configureServices(services);

        var serviceProvider = services.BuildServiceProvider();

		var context = serviceProvider.GetRequiredService<NotebookStoreContext.NotebookStoreContext>();

		context.Database.EnsureCreated();

		return new ComponentsContext(serviceProvider, context);
	}

	public class ComponentsContext : IDisposable
	{
        private readonly ServiceProvider serviceProvider;
        private readonly NotebookStoreContext.NotebookStoreContext context;

        public ComponentsContext(ServiceProvider serviceProvider, NotebookStoreContext.NotebookStoreContext context)		
		{
            this.serviceProvider = serviceProvider;
            this.context = context;
        }

		public T Resolve<T>() where T : class
		{
			return serviceProvider.GetRequiredService<T>();
		}

		public void Dispose()
		{
			context.Database.EnsureDeleted();
			context.Dispose();
			serviceProvider.Dispose();
		}
	}
}