using IbmImporter;
using IbmImporter.Models;
using Moq;

namespace ImporterTests;

public class IbmImporterTests
{
    [OneTimeSetUp]
    public void Setup()
    {
    }

    [Test]
    public void DataImporter_Import_ReturnsImportResultSuccessfully()
    {
        using var context = TestStartup.CreateComponentsContext();

        var sut = context.Resolve<DataImporter>();

        // Arrange
        Mock.Get(context.Resolve<IJsonFileParser>())
            .Setup(x => x.Parse(It.IsAny<string>()))
            .Returns(new NotebookData
            {
                Customer = "Customer",
                Notebooks = new List<Notebook>
                {
                    new() {
                        Quantity = 1,
                        Name = "Name",
                        Price = 1700,
                        CPU = 1,
                        Color = "Color",
                        DateOfProduction = DateTime.Now,
                        Ram = 1,
                        ProcessorModel = "ProcessorModel",
                        Monitor = new IbmImporter.Models.Monitor()
                        {
                            Height = 1,
                            Width = 1,
                            SupportedRefreshRate = { 1 }
                        },
                        Ports = new Ports()
                        {
                            Hdmi = 1,
                            Usb = 1
                        }
                    }
                }
            });

        // Act
        var result = sut.Import("");

        // Assert
        Assert.Multiple(() =>
        {

            Assert.That(result.Success, Is.EqualTo(1));
            Assert.That(result.Unsuccess, Is.Empty);
        });
    }
}
