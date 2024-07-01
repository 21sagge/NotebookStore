using Moq;
using NotebookStore.DAL;
using AutoMapper;
using NotebookStore.Entities;
using NotebookStoreMVC;
using Microsoft.EntityFrameworkCore;

namespace NotebookStore.Business.Tests;

[TestFixture]
public class BrandServiceTests
{
    private IUnitOfWork unitOfWork;
    private IMapper mapper;
    private Mock<IUserService> mockUserService;
    private Mock<IPermissionService> mockPermissionService;
    private BrandService brandService;

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

        brandService = new BrandService
        (
            unitOfWork,
            mapper,
            mockUserService.Object,
            mockPermissionService.Object
        );
    }

    [Test]
    public async Task GetAll_ReturnsBrands()
    {
        // Arrange
        var brands = new List<Brand>
        {
            new()
            {
                Id = 1,
                Name = "Brand 1",
                CreatedAt = DateTime.Now.ToString(),
                CreatedBy = null
            },
            new()
            {
                Id = 2,
                Name = "Brand 2",
                CreatedAt = DateTime.Now.ToString(),
                CreatedBy = null
            }
        };

        var currentUser = new User
        {
            Id = 1,
            Name = "User 1",
            Password = "admin",
            Email = "admin@admin.com",
        };

        mockUserService.Setup(x => x.GetCurrentUser()).ReturnsAsync(new UserDto
        {
            Id = currentUser.Id.ToString(),
            Name = currentUser.Name,
            Email = currentUser.Email,
            Password = currentUser.Password,
            Role = "Admin"
        });

        mockPermissionService.Setup(x => x.CanUpdateBrand(It.IsAny<Brand>(), It.IsAny<UserDto>())).Returns(true);

        // Add brands to the database
        await unitOfWork.Brands.Create(brands[0]);
        await unitOfWork.Brands.Create(brands[1]);

        // Act
        var result = await brandService.GetAll();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.That(result.ElementAt(0).Id, Is.EqualTo(1));
            Assert.That(result.ElementAt(0).Name, Is.EqualTo("Brand 1"));
            Assert.That(result.ElementAt(0).CanUpdate, Is.True);
            Assert.That(result.ElementAt(0).CanDelete, Is.True);

            Assert.That(result.ElementAt(1).Id, Is.EqualTo(2));
            Assert.That(result.ElementAt(1).Name, Is.EqualTo("Brand 2"));
            Assert.That(result.ElementAt(1).CanUpdate, Is.True);
            Assert.That(result.ElementAt(1).CanDelete, Is.True);
        });
    }

    [Test]
    public async Task Find_ReturnsBrand()
    {
        // Arrange
        var brand = new Brand
        {
            Id = 1,
            Name = "Brand 1",
            CreatedAt = DateTime.Now.ToString(),
            CreatedBy = null
        };

        var currentUser = new User
        {
            Id = 1,
            Name = "User 1",
            Password = "password",
            Email = "admin@admin.com",
        };

        mockUserService.Setup(x => x.GetCurrentUser()).ReturnsAsync(new UserDto
        {
            Id = currentUser.Id.ToString(),
            Name = currentUser.Name,
            Email = currentUser.Email,
            Password = currentUser.Password,
            Role = "Admin"
        });

        mockPermissionService.Setup(x => x.CanUpdateBrand(It.IsAny<Brand>(), It.IsAny<UserDto>())).Returns(true);

        // Add brand to the database
        await unitOfWork.Brands.Create(brand);

        // Act
        var result = await brandService.Find(1);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result?.Id, Is.EqualTo(1));
            Assert.That(result?.Name, Is.EqualTo("Brand 1"));
            Assert.That(result?.CanUpdate, Is.True);
            Assert.That(result?.CanDelete, Is.True);
        });
    }

    [Test]
    public async Task Create_ReturnsBrand()
    {
        // Arrange
        var brand = new Brand
        {
            Id = 1,
            Name = "Brand 1",
            CreatedAt = DateTime.Now.ToString(),
            CreatedBy = null
        };

        var currentUser = new User
        {
            Id = 1,
            Name = "User 1",
            Password = "password",
            Email = "",
        };

        mockUserService.Setup(x => x.GetCurrentUser()).ReturnsAsync(new UserDto
        {
            Id = currentUser.Id.ToString(),
            Name = currentUser.Name,
            Email = currentUser.Email,
            Password = currentUser.Password,
            Role = "Admin"
        });

        mockPermissionService.Setup(x => x.CanUpdateBrand(It.IsAny<Brand>(), It.IsAny<UserDto>())).Returns(true);

        // Act
        await unitOfWork.Brands.Create(brand);

        // Assert that the brand was added to the database
        var addedBrand = await unitOfWork.Brands.Find(1);

        Assert.Multiple(() =>
        {
            Assert.That(addedBrand?.Id, Is.EqualTo(1));
            Assert.That(addedBrand?.Name, Is.EqualTo("Brand 1"));
        });
    }
}
