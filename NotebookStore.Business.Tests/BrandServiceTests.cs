using Moq;
using NotebookStore.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace NotebookStore.Business.Tests;

[TestFixture]
[Parallelizable(ParallelScope.All)]
public class BrandServiceTests
{
    private BrandService sut;
    private Mock<IUserService> mockUserService;
    private Mock<IPermissionService> mockPermissionService;
    private NotebookStoreContext.NotebookStoreContext context;

    [SetUp]
    public void Setup()
    {
        var testStartup = new TestStartup();

        mockUserService = new Mock<IUserService>();
        mockPermissionService = new Mock<IPermissionService>();

        testStartup.Register<BrandService>();
        testStartup.Register(mockUserService.Object);
        testStartup.Register(mockPermissionService.Object);

        sut = testStartup.Resolve<BrandService>();

        context = testStartup.Resolve<NotebookStoreContext.NotebookStoreContext>();
        context.Database.EnsureCreated();
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
        var brand1Result = result.FirstOrDefault(b => b.Id == brand1.Id);
        var brand2Result = result.FirstOrDefault(b => b.Id == brand2.Id);

        // Assert
        Assert.That(result.Count, Is.EqualTo(2), "Brands count should be 2");

        Assert.Multiple(() =>
        {
            Assert.That(brand1Result?.Id, Is.EqualTo(1), "Brand 1 Id should be 1");
            Assert.That(brand1Result?.Name, Is.EqualTo("Brand 1"), "Brand 1 Name should be Brand 1");
            Assert.That(brand1Result?.CanUpdate, Is.True, "Brand 1 CanUpdate should be true");
            Assert.That(brand1Result?.CanDelete, Is.True, "Brand 1 CanDelete should be true");
        });

        Assert.Multiple(() =>
        {
            Assert.That(brand2Result?.Id, Is.EqualTo(2), "Brand 2 Id should be 2");
            Assert.That(brand2Result?.Name, Is.EqualTo("Brand 2"), "Brand 2 Name should be Brand 2");
            Assert.That(brand2Result?.CanUpdate, Is.True, "Brand 2 CanUpdate should be true");
            Assert.That(brand2Result?.CanDelete, Is.True, "Brand 2 CanDelete should be true");
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
        var result = await sut.Find(brand.Id);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result?.Id, Is.EqualTo(1), "Brand Id should be 1");
            Assert.That(result?.Name, Is.EqualTo("Brand 1"), "Brand Name should be Brand 1");
            Assert.That(result?.CanUpdate, Is.True, "Brand CanUpdate should be true");
            Assert.That(result?.CanDelete, Is.True, "Brand CanDelete should be true");
        });
    }

    [Test]
    public async Task Create_ReturnsBrand()
    {
        // Arrange
        var brand = new BrandDto
        {
            Id = 1,
            Name = "Brand 1"
        };

        mockUserService.Setup(x => x.GetCurrentUser()).ReturnsAsync(new UserDto
        {
            Id = "1",
            Name = "User 1",
            Email = "",
            Password = "",
            Role = "Admin"
        });

        mockPermissionService.Setup(
            x => x.CanUpdateBrand(
                It.IsAny<Brand>(),
                It.IsAny<UserDto>()))
            .Returns(true);

        // Act
        var result = await sut.Create(brand);
        var addedBrand = context.Brands.FirstOrDefault(b => b.Id == brand.Id);

        Assert.Multiple(() =>
        {
            Assert.That(addedBrand?.Id, Is.EqualTo(1), "Brand Id should be 1");
            Assert.That(addedBrand?.Name, Is.EqualTo("Brand 1"), "Brand Name should be Brand 1");
            Assert.That(addedBrand?.CreatedAt, Is.Not.Null, "Brand CreatedAt should not be null");
            Assert.That(addedBrand?.CreatedBy, Is.EqualTo("1"), "Brand CreatedBy should be 1");
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

        mockUserService.Setup(x => x.GetCurrentUser())
        .ReturnsAsync(
            new UserDto
            {
                Id = "1",
                Name = "User 1",
                Email = "",
                Password = "",
                Role = "Admin"
            }
        );

        mockPermissionService.Setup(
            x => x.CanUpdateBrand(
                It.IsAny<Brand>(),
                It.IsAny<UserDto>()))
            .Returns(true);

        context.Add(brand);
        context.SaveChanges();

        // Act
        var brandDto = new BrandDto()
        {
            Id = brand.Id,
            Name = "Brand 2",
            CanDelete = true,
            CanUpdate = true
        };

        var result = await sut.Update(brandDto);
        var updatedBrand = context.Brands.FirstOrDefault(b => b.Id == brand.Id);

        Assert.Multiple(() =>
        {
            Assert.That(updatedBrand?.Id, Is.EqualTo(1), "Brand Id should be 1");
            Assert.That(updatedBrand?.Name, Is.EqualTo("Brand 2"), "Brand Name should be Brand 2");
            Assert.That(updatedBrand?.CreatedAt, Is.Not.Null, "Brand CreatedAt should not be null");
            Assert.That(updatedBrand?.CreatedBy, Is.Null, "Brand CreatedBy should be null");
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

        mockPermissionService.Setup(
            x => x.CanUpdateBrand(
                It.IsAny<Brand>(),
                It.IsAny<UserDto>()))
            .Returns(true);

        context.Add(brand);
        context.SaveChanges();

        // Act
        var result = await sut.Delete(brand.Id);
        var deletedBrand = context.Brands.FirstOrDefault(b => b.Id == brand.Id);

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.True, "Delete should return true");
            Assert.That(deletedBrand, Is.Null, "Brand should be deleted");
        });
    }

    [TearDown]
    public void TearDown()
    {
        context.Database.EnsureDeleted();
        context.Dispose();
    }
}
