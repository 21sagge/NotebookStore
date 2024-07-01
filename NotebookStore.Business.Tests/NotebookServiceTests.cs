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

		context.Database.EnsureDeleted();
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

		foreach (var notebook in notebooks)
		{
			await unitOfWork.Notebooks.Create(notebook);
		}

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
