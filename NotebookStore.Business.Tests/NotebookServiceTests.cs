using Moq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NotebookStore.DAL;
using NotebookStore.Entities;
using NotebookStoreMVC;

namespace NotebookStore.Business.Tests;

public class NotebookServiceTests
{
	private IUnitOfWork unitOfWork;
	private IMapper mapper;
	private Mock<IUserService> mockUserService;
	private Mock<IPermissionService> mockPermissionService;
	private NotebookService notebookService;

	[SetUp]
	public void Setup()
	{
		var options = new DbContextOptionsBuilder<NotebookStoreContext.NotebookStoreContext>()
			.UseSqlite("DataSource=notebookStoreTest.db")
			.Options;

		var context = new NotebookStoreContext.NotebookStoreContext(options);

		context.Database.EnsureCreated();

		unitOfWork = new UnitOfWork(context);
		mapper = new MapperConfiguration(cfg => cfg.AddProfile<MapperMvc>()).CreateMapper();
		mockUserService = new Mock<IUserService>();
		mockPermissionService = new Mock<IPermissionService>();

		notebookService = new NotebookService
		(
			unitOfWork,
			mapper,
			mockUserService.Object,
			mockPermissionService.Object
		);
	}

	[Test]
	public async Task GetAll_ReturnsNotebooks()
	{
		// Arrange
		var notebooks = new List<Notebook>
		{
			new()
			{
				Id = 1,
				BrandId = 1,
				Brand = new Brand
				{
					Id = 1,
					Name = "Brand 1",
					CreatedAt = DateTime.Now.ToString(),
				},
				ModelId = 1,
				Model = new Model
				{
					Id = 1,
					Name = "Model 1",
					CreatedAt = DateTime.Now.ToString(),
				},
				CpuId = 1,
				Cpu = new Cpu
				{
					Id = 1,
					Brand = "Intel",
					Model = "Core i5",
					CreatedAt = DateTime.Now.ToString(),
				},
				DisplayId = 1,
				Display = new Display
				{
					Id = 1,
					Size = 15.6,
					ResolutionWidth = 1920,
					ResolutionHeight = 1080,
					PanelType = "IPS",
					CreatedAt = DateTime.Now.ToString(),
				},
				MemoryId = 1,
				Memory = new Memory
				{
					Id = 1,
					Capacity = 8,
					Speed = 3200,
					CreatedAt = DateTime.Now.ToString(),
				},
				StorageId = 1,
				Storage = new Storage
				{
					Id = 1,
					Capacity = 512,
					Type = "SSD",
					CreatedAt = DateTime.Now.ToString(),
				},
				Color = "Black",
				CreatedAt = DateTime.Now.ToString(),
				CreatedBy = null
			},
			new()
			{
				Id = 2,
				BrandId = 2,
				Brand = new Brand
				{
					Id = 2,
					Name = "Brand 2",
					CreatedAt = DateTime.Now.ToString(),
				},
				ModelId = 2,
				Model = new Model
				{
					Id = 2,
					Name = "Model 2",
					CreatedAt = DateTime.Now.ToString(),
				},
				CpuId = 2,
				Cpu = new Cpu
				{
					Id = 2,
					Brand = "AMD",
					Model = "Ryzen 5",
					CreatedAt = DateTime.Now.ToString(),
				},
				DisplayId = 2,
				Display = new Display
				{
					Id = 2,
					Size = 14,
					ResolutionWidth = 1920,
					ResolutionHeight = 1080,
					PanelType = "IPS",
					CreatedAt = DateTime.Now.ToString(),
				},
				MemoryId = 2,
				Memory = new Memory
				{
					Id = 2,
					Capacity = 16,
					Speed = 3200,
					CreatedAt = DateTime.Now.ToString(),
				},
				StorageId = 2,
				Storage = new Storage
				{
					Id = 2,
					Capacity = 1024,
					Type = "SSD",
					CreatedAt = DateTime.Now.ToString(),
				},
				Color = "White",
				CreatedAt = DateTime.Now.ToString(),
				CreatedBy = null
			}
		};

		var currentUser = new User
		{
			Id = 1,
			Name = "user1",
			Email = "",
			Password = "password"
		};

		mockUserService.Setup(service => service.GetCurrentUser()).ReturnsAsync(
			new UserDto
			{
				Id = currentUser.Id.ToString(),
				Name = currentUser.Name,
				Email = currentUser.Email,
				Password = currentUser.Password,
				Role = "Admin"
			}
		);

		// Delete all notebooks from the database
		var allNotebooks = await unitOfWork.Notebooks.Read();
		foreach (var notebook in allNotebooks)
		{
			await unitOfWork.Notebooks.Delete(notebook.Id);
		}

		// Delete all brands from the database
		var allBrands = await unitOfWork.Brands.Read();
		foreach (var brand in allBrands)
		{
			await unitOfWork.Brands.Delete(brand.Id);
		}

		// Delete all models from the database
		var allModels = await unitOfWork.Models.Read();
		foreach (var model in allModels)
		{
			await unitOfWork.Models.Delete(model.Id);
		}

		// Delete all cpus from the database
		var allCpus = await unitOfWork.Cpus.Read();
		foreach (var cpu in allCpus)
		{
			await unitOfWork.Cpus.Delete(cpu.Id);
		}

		// Delete all displays from the database
		var allDisplays = await unitOfWork.Displays.Read();
		foreach (var display in allDisplays)
		{
			await unitOfWork.Displays.Delete(display.Id);
		}

		// Delete all memories from the database
		var allMemories = await unitOfWork.Memories.Read();
		foreach (var memory in allMemories)
		{
			await unitOfWork.Memories.Delete(memory.Id);
		}

		// Delete all storages from the database
		var allStorages = await unitOfWork.Storages.Read();
		foreach (var storage in allStorages)
		{
			await unitOfWork.Storages.Delete(storage.Id);
		}

		// Add brands to the database
		await unitOfWork.Notebooks.Create(notebooks[0]);
		await unitOfWork.Notebooks.Create(notebooks[1]);

		// Act
		var result = await notebookService.GetAll();

		// Assert
		Assert.That(result.Count(), Is.EqualTo(notebooks.Count));
	}

	[Test]
	public async Task Find_ReturnsNotebook()
	{
		// Arrange
		var notebook = new Notebook
		{
			Id = 1,
			BrandId = 1,
			Brand = new Brand
			{
				Id = 1,
				Name = "Brand 1",
				CreatedAt = DateTime.Now.ToString(),
			},
			ModelId = 1,
			Model = new Model
			{
				Id = 1,
				Name = "Model 1",
				CreatedAt = DateTime.Now.ToString(),
			},
			CpuId = 1,
			Cpu = new Cpu
			{
				Id = 1,
				Brand = "Intel",
				Model = "Core i5",
				CreatedAt = DateTime.Now.ToString(),
			},
			DisplayId = 1,
			Display = new Display
			{
				Id = 1,
				Size = 15.6,
				ResolutionWidth = 1920,
				ResolutionHeight = 1080,
				PanelType = "IPS",
				CreatedAt = DateTime.Now.ToString(),
			},
			MemoryId = 1,
			Memory = new Memory
			{
				Id = 1,
				Capacity = 8,
				Speed = 3200,
				CreatedAt = DateTime.Now.ToString(),
			},
			StorageId = 1,
			Storage = new Storage
			{
				Id = 1,
				Capacity = 512,
				Type = "SSD",
				CreatedAt = DateTime.Now.ToString(),
			},
			Color = "Black",
			CreatedAt = DateTime.Now.ToString(),
			CreatedBy = null
		};

		var currentUser = new User
		{
			Id = 1,
			Name = "user1",
			Email = "",
			Password = "password"
		};

		mockUserService.Setup(service => service.GetCurrentUser())
		.ReturnsAsync(
			new UserDto
			{
				Id = currentUser.Id.ToString(),
				Name = currentUser.Name,
				Email = currentUser.Email,
				Password = currentUser.Password,
				Role = "Admin"
			}
		);

		mockPermissionService.Setup(
			service => service.CanUpdateNotebook(
				It.IsAny<Notebook>(),
				It.IsAny<UserDto>()))
			.Returns(true);

		// Delete all notebooks from the database
		var allNotebooks = await unitOfWork.Notebooks.Read();
		foreach (var n in allNotebooks)
		{
			await unitOfWork.Notebooks.Delete(n.Id);
		}

		// Delete all brands from the database
		var allBrands = await unitOfWork.Brands.Read();
		foreach (var brand in allBrands)
		{
			await unitOfWork.Brands.Delete(brand.Id);
		}

		// Delete all models from the database
		var allModels = await unitOfWork.Models.Read();
		foreach (var model in allModels)
		{
			await unitOfWork.Models.Delete(model.Id);
		}

		// Delete all cpus from the database
		var allCpus = await unitOfWork.Cpus.Read();
		foreach (var cpu in allCpus)
		{
			await unitOfWork.Cpus.Delete(cpu.Id);
		}

		// Delete all displays from the database
		var allDisplays = await unitOfWork.Displays.Read();
		foreach (var display in allDisplays)
		{
			await unitOfWork.Displays.Delete(display.Id);
		}

		// Delete all memories from the database
		var allMemories = await unitOfWork.Memories.Read();
		foreach (var memory in allMemories)
		{
			await unitOfWork.Memories.Delete(memory.Id);
		}

		// Delete all storages from the database
		var allStorages = await unitOfWork.Storages.Read();
		foreach (var storage in allStorages)
		{
			await unitOfWork.Storages.Delete(storage.Id);
		}

		await unitOfWork.Notebooks.Create(notebook);

		// Act
		var result = await notebookService.Find(notebook.Id);

		// Assert
		Assert.Multiple(() =>
		{
			Assert.That(result?.Id, Is.EqualTo(notebook.Id));
			Assert.That(result?.BrandId, Is.EqualTo(notebook.BrandId));
			Assert.That(result?.Color, Is.EqualTo(notebook.Color));
			Assert.That(result?.CanUpdate, Is.True);
			Assert.That(result?.CanDelete, Is.True);
		});
	}
}
