using Moq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NotebookStore.DAL;
using NotebookStore.Entities;
using NotebookStore.Business.Mapping;
using System.Runtime.InteropServices;

namespace NotebookStore.Business.Tests;

public class NotebookServiceTests
{
    private IUnitOfWork unitOfWork;
    private IMapper mapper;
    private Mock<IUserService> mockUserService;
    private Mock<IPermissionService> mockPermissionService;
    private NotebookService sut;
    private NotebookStoreContext.NotebookStoreContext context;

    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<NotebookStoreContext.NotebookStoreContext>()
            .UseSqlite("DataSource=notebookStoreTest.db")
            .Options;

        context = new NotebookStoreContext.NotebookStoreContext(options);

        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        unitOfWork = new UnitOfWork(context);
        mapper = new MapperConfiguration(cfg => cfg.AddProfile<BusinessMapper>()).CreateMapper();
        mockUserService = new Mock<IUserService>();
        mockPermissionService = new Mock<IPermissionService>();

        sut = new NotebookService
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
        var notebook1 = new Notebook
        {
            Id = 1,
            Brand = new Brand
            {
                Id = 1,
                Name = "Brand 1",
                CreatedAt = DateTime.Now.ToString(),
            },
            BrandId = 1,
            Model = new Model
            {
                Id = 1,
                Name = "Model 1",
                CreatedAt = DateTime.Now.ToString(),
            },
            ModelId = 1,
            Cpu = new Cpu
            {
                Id = 1,
                Brand = "Intel",
                Model = "Core i5",
                CreatedAt = DateTime.Now.ToString(),
            },
            CpuId = 1,
            Display = new Display
            {
                Id = 1,
                Size = 15.6,
                ResolutionWidth = 1920,
                ResolutionHeight = 1080,
                PanelType = "IPS",
                CreatedAt = DateTime.Now.ToString(),
            },
            DisplayId = 1,
            Memory = new Memory
            {
                Id = 1,
                Capacity = 8,
                Speed = 3200,
                CreatedAt = DateTime.Now.ToString(),
            },
            MemoryId = 1,
            Storage = new Storage
            {
                Id = 1,
                Capacity = 512,
                Type = "SSD",
                CreatedAt = DateTime.Now.ToString(),
            },
            StorageId = 1,
            Color = "Black",
            CreatedAt = DateTime.Now.ToString(),
            CreatedBy = null
        };

        var notebook2 = new Notebook
        {
            Id = 2,
            Brand = new Brand
            {
                Id = 2,
                Name = "Brand 2",
                CreatedAt = DateTime.Now.ToString(),
            },
            BrandId = 2,
            Model = new Model
            {
                Id = 2,
                Name = "Model 2",
                CreatedAt = DateTime.Now.ToString(),
            },
            ModelId = 2,
            Cpu = new Cpu
            {
                Id = 2,
                Brand = "AMD",
                Model = "Ryzen 5",
                CreatedAt = DateTime.Now.ToString(),
            },
            CpuId = 2,
            Display = new Display
            {
                Id = 2,
                Size = 14,
                ResolutionWidth = 1920,
                ResolutionHeight = 1080,
                PanelType = "IPS",
                CreatedAt = DateTime.Now.ToString(),
            },
            DisplayId = 2,
            Memory = new Memory
            {
                Id = 2,
                Capacity = 16,
                Speed = 3200,
                CreatedAt = DateTime.Now.ToString(),
            },
            MemoryId = 2,
            Storage = new Storage
            {
                Id = 2,
                Capacity = 1024,
                Type = "SSD",
                CreatedAt = DateTime.Now.ToString(),
            },
            StorageId = 2,
            Color = "White",
            CreatedAt = DateTime.Now.ToString(),
            CreatedBy = null
        };

        mockUserService.Setup(service => service.GetCurrentUser()).ReturnsAsync(
            new UserDto
            {
                Id = "1",
                Name = "user1",
                Email = "",
                Password = "",
                Role = "Admin"
            }
        );

        mockPermissionService.Setup(
            service => service.CanUpdateNotebook(
                It.IsAny<Notebook>(),
                It.IsAny<UserDto>()))
            .Returns(true);

        context.AddRange(notebook1, notebook2);
        context.SaveChanges();

        // Act
        var result = await sut.GetAll();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result.ElementAt(0).Id, Is.EqualTo(notebook1.Id));
            Assert.That(result.ElementAt(1).Id, Is.EqualTo(notebook2.Id));

            Assert.That(result.ElementAt(0).CanUpdate, Is.True);
            Assert.That(result.ElementAt(0).CanDelete, Is.True);
            Assert.That(result.ElementAt(1).CanUpdate, Is.True);
            Assert.That(result.ElementAt(1).CanDelete, Is.True);

            Assert.That(result.ElementAt(0).BrandId, Is.EqualTo(notebook1.BrandId));
            Assert.That(result.ElementAt(0).ModelId, Is.EqualTo(notebook1.ModelId));
            Assert.That(result.ElementAt(0).CpuId, Is.EqualTo(notebook1.CpuId));
            Assert.That(result.ElementAt(0).DisplayId, Is.EqualTo(notebook1.DisplayId));
            Assert.That(result.ElementAt(0).MemoryId, Is.EqualTo(notebook1.MemoryId));
            Assert.That(result.ElementAt(0).StorageId, Is.EqualTo(notebook1.StorageId));

            Assert.That(result.ElementAt(1).BrandId, Is.EqualTo(notebook2.BrandId));
            Assert.That(result.ElementAt(1).ModelId, Is.EqualTo(notebook2.ModelId));
            Assert.That(result.ElementAt(1).CpuId, Is.EqualTo(notebook2.CpuId));
            Assert.That(result.ElementAt(1).DisplayId, Is.EqualTo(notebook2.DisplayId));
            Assert.That(result.ElementAt(1).MemoryId, Is.EqualTo(notebook2.MemoryId));
            Assert.That(result.ElementAt(1).StorageId, Is.EqualTo(notebook2.StorageId));

            Assert.That(result.ElementAt(0).Color, Is.EqualTo(notebook1.Color));
            Assert.That(result.ElementAt(1).Color, Is.EqualTo(notebook2.Color));

            Assert.That(result.ElementAt(0).Brand?.Name, Is.EqualTo(notebook1.Brand.Name));
            Assert.That(result.ElementAt(0).Model?.Name, Is.EqualTo(notebook1.Model.Name));
            Assert.That(result.ElementAt(0).Cpu?.Brand, Is.EqualTo(notebook1.Cpu.Brand));
            Assert.That(result.ElementAt(0).Display?.Size, Is.EqualTo(notebook1.Display.Size));
            Assert.That(result.ElementAt(0).Memory?.Capacity, Is.EqualTo(notebook1.Memory.Capacity));
            Assert.That(result.ElementAt(0).Storage?.Capacity, Is.EqualTo(notebook1.Storage.Capacity));

            Assert.That(result.ElementAt(1).Brand?.Name, Is.EqualTo(notebook2.Brand.Name));
            Assert.That(result.ElementAt(1).Model?.Name, Is.EqualTo(notebook2.Model?.Name));
            Assert.That(result.ElementAt(1).Cpu?.Brand, Is.EqualTo(notebook2.Cpu?.Brand));
            Assert.That(result.ElementAt(1).Display?.Size, Is.EqualTo(notebook2.Display?.Size));
            Assert.That(result.ElementAt(1).Memory?.Capacity, Is.EqualTo(notebook2.Memory?.Capacity));
            Assert.That(result.ElementAt(1).Storage?.Capacity, Is.EqualTo(notebook2.Storage?.Capacity));
        });
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

        mockUserService.Setup(service => service.GetCurrentUser())
        .ReturnsAsync(
            new UserDto
            {
                Id = "1",
                Name = "user1",
                Email = "",
                Password = "",
                Role = "Admin"
            }
        );

        mockPermissionService.Setup(
            service => service.CanUpdateNotebook(
                It.IsAny<Notebook>(),
                It.IsAny<UserDto>()))
            .Returns(true);

        context.Add(notebook);
        context.SaveChanges();

        // Act
        var result = await sut.Find(notebook.Id);

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
