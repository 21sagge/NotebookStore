using Moq;
using NUnit.Framework;
using NotebookStore.Business;
using NotebookStore.DAL;
using AutoMapper;
using NotebookStore.Entities;

namespace NotebookStore.Business.Tests;

[TestFixture]
public class BrandServiceTests
{
    private Mock<IUnitOfWork> mockUnitOfWork;
    private Mock<IMapper> mockMapper;
    private Mock<IUserService> mockUserService;
    private Mock<IPermissionService> mockPermissionService;
    private BrandService brandService;

    [SetUp]
    public void Setup()
    {
        mockUnitOfWork = new Mock<IUnitOfWork>();
        mockMapper = new Mock<IMapper>();
        mockUserService = new Mock<IUserService>();
        mockPermissionService = new Mock<IPermissionService>();
        
        brandService = new BrandService
        (
            mockUnitOfWork.Object,
            mockMapper.Object,
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
                CreatedBy = "1"
            },
            new()
            {
                Id = 2,
                Name = "Brand 2",
                CreatedAt = DateTime.Now.ToString(),
                CreatedBy = "2"
            }
        };

        var currentUser = new User
        {
            Id = 1,
            Name = "User 1",
            Password = "password",
            Email = "admin"
        };

        mockUserService.Setup(u => u.GetCurrentUser()).ReturnsAsync(currentUser);

        mockPermissionService.Setup(p => p.CanUpdateBrand(It.IsAny<Brand>(), currentUser)).Returns(true);

        mockMapper.Setup(m => m.Map<BrandDto>(It.IsAny<Brand>()))
            .Returns<Brand>(b => new BrandDto { Id = b.Id, Name = b.Name });

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
}
