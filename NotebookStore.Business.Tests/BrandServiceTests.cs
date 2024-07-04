using Moq;
using NotebookStore.DAL;
using AutoMapper;
using NotebookStore.Entities;
using Microsoft.EntityFrameworkCore;
using NotebookStore.Business.Mapping;

namespace NotebookStore.Business.Tests;

[TestFixture]
public class BrandServiceTests
{
    private IUnitOfWork unitOfWork;
    private IMapper mapper;
    private Mock<IUserService> mockUserService;
    private Mock<IPermissionService> mockPermissionService;
    private BrandService sut;
    private NotebookStoreContext.NotebookStoreContext context;

    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<NotebookStoreContext.NotebookStoreContext>()
            .UseSqlite("DataSource=notebookStoreTest.db")
            .Options;

        context = new NotebookStoreContext.NotebookStoreContext(options);

        context.Database.EnsureCreated();

        unitOfWork = new UnitOfWork(context);
        mapper = new MapperConfiguration(cfg => cfg.AddProfile<BusinessMapper>()).CreateMapper();
        mockUserService = new Mock<IUserService>();
        mockPermissionService = new Mock<IPermissionService>();

        sut = new BrandService
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
        var brand1 = new Brand
        {
            Id = 1,
            Name = "Brand 1",
            CreatedAt = DateTime.Now.ToString(),
            CreatedBy = null
        };

        var brand2 = new Brand
        {
            Id = 2,
            Name = "Brand 2",
            CreatedAt = DateTime.Now.ToString(),
            CreatedBy = null
        };

        mockUserService.Setup(x => x.GetCurrentUser()).ReturnsAsync(new UserDto
        {
            Id = "1",
            Name = "User 1",
            Email = "",
            Password = "",
            Role = "Admin"
        });

        mockPermissionService.Setup(x => x.CanUpdateBrand(It.IsAny<Brand>(), It.IsAny<UserDto>())).Returns(true);

        context.AddRange(brand1, brand2);
        context.SaveChanges();

        // Act
        var result = await sut.GetAll();

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

        mockUserService.Setup(x => x.GetCurrentUser()).ReturnsAsync(new UserDto
        {
            Id = "1",
            Name = "User 1",
            Email = "",
            Password = "",
            Role = "Admin"
        });

        mockPermissionService.Setup(x => x.CanUpdateBrand(It.IsAny<Brand>(), It.IsAny<UserDto>())).Returns(true);

        context.Add(brand);
        context.SaveChanges();

        // Act
        var result = await sut.Find(1);

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

        mockUserService.Setup(x => x.GetCurrentUser()).ReturnsAsync(new UserDto
        {
            Id = "1",
            Name = "User 1",
            Email = "",
            Password = "",
            Role = "Admin"
        });

        mockPermissionService.Setup(x => x.CanUpdateBrand(It.IsAny<Brand>(), It.IsAny<UserDto>())).Returns(true);

        // Act
        await unitOfWork.Brands.Create(brand);

        var addedBrand = context.Brands.FirstOrDefault(b => b.Id == brand.Id);

        Assert.Multiple(() =>
        {
            Assert.That(addedBrand?.Id, Is.EqualTo(1));
            Assert.That(addedBrand?.Name, Is.EqualTo("Brand 1"));
            Assert.That(addedBrand?.CreatedAt, Is.Not.Null);
            Assert.That(addedBrand?.CreatedBy, Is.Null);
        });
    }

    [Test]
    public async Task Update_ReturnsBrand()
    {
        // Arrange
        var brand = new Brand
        {
            Id = 1,
            Name = "Brand 1",
            CreatedAt = DateTime.Now.ToString(),
            CreatedBy = null
        };

        mockUserService.Setup(x => x.GetCurrentUser()).ReturnsAsync(new UserDto
        {
            Id = "1",
            Name = "User 1",
            Email = "",
            Password = "",
            Role = "Admin"
        });

        mockPermissionService.Setup(x => x.CanUpdateBrand(It.IsAny<Brand>(), It.IsAny<UserDto>())).Returns(true);

        context.Add(brand);
        context.SaveChanges();

        // Act
        brand.Name = "Brand 2";
        await unitOfWork.Brands.Update(brand);

        var updatedBrand = context.Brands.FirstOrDefault(b => b.Id == brand.Id);

        Assert.Multiple(() =>
        {
            Assert.That(updatedBrand?.Id, Is.EqualTo(1));
            Assert.That(updatedBrand?.Name, Is.EqualTo("Brand 2"));
            Assert.That(updatedBrand?.CreatedAt, Is.Not.Null);
            Assert.That(updatedBrand?.CreatedBy, Is.Null);
        });
    }

    [Test]
    public async Task Delete_ReturnsBrand()
    {
        // Arrange
        var brand = new Brand
        {
            Id = 1,
            Name = "Brand 1",
            CreatedAt = DateTime.Now.ToString(),
            CreatedBy = null
        };

        mockUserService.Setup(x => x.GetCurrentUser()).ReturnsAsync(new UserDto
        {
            Id = "1",
            Name = "User 1",
            Email = "",
            Password = "",
            Role = "Admin"
        });

        mockPermissionService.Setup(x => x.CanUpdateBrand(It.IsAny<Brand>(), It.IsAny<UserDto>())).Returns(true);

        context.Add(brand);
        context.SaveChanges();

        // Act
        await unitOfWork.Brands.Delete(brand.Id);

        var deletedBrand = context.Brands.FirstOrDefault(b => b.Id == brand.Id);

        Assert.That(deletedBrand, Is.Null);
    }

    [TearDown]
    public void TearDown()
    {
        context.Database.EnsureDeleted();
        context.Dispose();
    }
}
