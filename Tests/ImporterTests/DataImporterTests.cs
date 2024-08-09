using IbmImporter;
using IbmImporter.Models;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Monitor = IbmImporter.Models.Monitor;

namespace ImporterTests
{
	public class DataImporterTests
	{
        [Test]
        public async Task Import_WithValidData_ShouldReturnSuccess()
        {
            using var context = TestStartup.CreateComponentsContext(services =>
            {
                var mockParser = new Mock<IJsonFileParser>();

                mockParser
                    .Setup(x => x.Parse(It.IsAny<string>()))
                    .Returns(new NotebookData
                    {
                        Customer = "Customer",
                        Notebooks = new List<Notebook>
                        {
                        new()
                        {
                            Quantity = 1,
                            Name = "Name",
                            Price = 1,
                            CPU = 1,
                            Color = "Color",
                            DateOfProduction = DateTime.Now,
                            Ram = 1,
                            ProcessorModel = "ProcessorModel",
                            Monitor = new Monitor
                            {
                                Width = 1,
                                Height = 1,
                                SupportedRefreshRate = { 1, 2, 3 }
                            },
                            Ports = new Ports
                            {
                                Usb = 1,
                                Hdmi = 1
                            }
                        }
                        }
                    });

                services.AddScoped(_ => mockParser.Object);
            });

            var sut = context.Resolve<DataImporter>();

            var result = await sut.ImportAsync("");

            Assert.Multiple(() =>
            {
                Assert.That(result.Unsuccess, Is.Empty, result.Unsuccess.FirstOrDefault()?.ErrorMessage);
                Assert.That(result.Success, Is.EqualTo(1));
                Assert.That(context.Resolve<NotebookStoreContext.NotebookStoreContext>().Notebooks.Count(), Is.EqualTo(1));
                Assert.That(context.Resolve<NotebookStoreContext.NotebookStoreContext>().Notebooks.First().Brand?.Name, Is.EqualTo("Name"));
            });
        }

        [Test]
        public async Task Import_WithInvalidData_ShouldReturnUnsuccess()
        {
            using var context = TestStartup.CreateComponentsContext(services =>
            {
                var mockParser = new Mock<IJsonFileParser>();

                mockParser
                    .Setup(x => x.Parse(It.IsAny<string>()))
                    .Returns(new NotebookData
                    {
                        Customer = "Customer",
                        // Invalid data - missing required fields
                        Notebooks = new List<Notebook> { new() }
                    });

                services.AddScoped(_ => mockParser.Object);
            });

            var sut = context.Resolve<DataImporter>();

            var result = await sut.ImportAsync("");

            Assert.Multiple(() =>
            {
                Assert.That(result.Unsuccess, Has.Count.EqualTo(1));
                Assert.That(result.Success, Is.EqualTo(0));
                Assert.That(context.Resolve<NotebookStoreContext.NotebookStoreContext>().Notebooks, Is.Empty);
                Assert.That(result.Unsuccess.First().ErrorMessage, Is.EqualTo("Name is null or empty"));
            });
        }

        [Test]
        public async Task Import_WithInvalidData_ShouldReturnUnsuccessAndSuccess()
        {
            using var context = TestStartup.CreateComponentsContext(services =>
            {
                var mockParser = new Mock<IJsonFileParser>();

                mockParser
                    .Setup(x => x.Parse(It.IsAny<string>()))
                    .Returns(new NotebookData
                    {
                        Customer = "Customer",
                        Notebooks = new List<Notebook>
                        {
                        new(),
                        new()
                        {
                            Quantity = 1,
                            Name = "Name",
                            Price = 1,
                            CPU = 1,
                            Color = "Color",
                            DateOfProduction = DateTime.Now,
                            Ram = 1,
                            ProcessorModel = "ProcessorModel",
                            Monitor = new Monitor
                            {
                                Width = 1,
                                Height = 1,
                                SupportedRefreshRate = { 1, 2, 3 }
                            },
                            Ports = new Ports
                            {
                                Usb = 1,
                                Hdmi = 1
                            }
                        }
                        }
                    });

                services.AddScoped(_ => mockParser.Object);
            });

            var sut = context.Resolve<DataImporter>();

            var result = await sut.ImportAsync("");

            Assert.Multiple(() =>
            {
                Assert.That(result.Unsuccess, Has.Count.EqualTo(1));
                Assert.That(result.Success, Is.EqualTo(1));
                Assert.That(context.Resolve<NotebookStoreContext.NotebookStoreContext>().Notebooks.Count(), Is.EqualTo(1));
                Assert.That(context.Resolve<NotebookStoreContext.NotebookStoreContext>().Notebooks.First().Brand?.Name, Is.EqualTo("Name"));
                Assert.That(result.Unsuccess.First().ErrorMessage, Is.EqualTo("Name is null or empty"));
            });
        }

        [Test]
        public async Task Import_WithInvalidData_ShouldReturnTwoUnsuccessAndOneSuccess()
        {
            using var context = TestStartup.CreateComponentsContext(services =>
            {
                var mockParser = new Mock<IJsonFileParser>();

                mockParser
                .Setup(x => x.Parse(It.IsAny<string>()))
                .Returns(new NotebookData
                {
                    Customer = "Customer",
                    Notebooks = new List<Notebook>
                    {
                    new(),
                    new()
                    {
                        Quantity = 1,
                        Name = "Name",
                        Price = 1,
                        CPU = 1,
                        Color = "Color",
                        DateOfProduction = DateTime.Now,
                        Ram = 1,
                        ProcessorModel = "ProcessorModel",
                        Monitor = new Monitor
                        {
                            Width = 1,
                            Height = 1,
                            SupportedRefreshRate = { 1, 2, 3 }
                        },
                        Ports = new Ports
                        {
                            Usb = 1,
                            Hdmi = 1
                        }
                    },
                    new()
                    }
                });

                services.AddScoped(_ => mockParser.Object);
            });

            var sut = context.Resolve<DataImporter>();

            var result = await sut.ImportAsync("");

            Assert.Multiple(() =>
            {
                Assert.That(result.Unsuccess, Has.Count.EqualTo(2));
                Assert.That(result.Success, Is.EqualTo(1));
                Assert.That(context.Resolve<NotebookStoreContext.NotebookStoreContext>().Notebooks.Count(), Is.EqualTo(1));
                Assert.That(context.Resolve<NotebookStoreContext.NotebookStoreContext>().Notebooks.First().Brand?.Name, Is.EqualTo("Name"));
                Assert.That(result.Unsuccess.First().ErrorMessage, Is.EqualTo("Name is null or empty"));
                Assert.That(result.Unsuccess.Last().ErrorMessage, Is.EqualTo("Name is null or empty"));
            });
        }

        [Test]
        public async Task Import_WithoutNotebooks_ShouldReturnUnsuccess()
        {
            using var context = TestStartup.CreateComponentsContext(services =>
            {
                var mockParser = new Mock<IJsonFileParser>();

                mockParser
                    .Setup(x => x.Parse(It.IsAny<string>()))
                    .Returns(new NotebookData
                    {
                        Customer = "Customer",
                        Notebooks = new List<Notebook>()
                    });

                services.AddScoped(_ => mockParser.Object);
            });

            var sut = context.Resolve<DataImporter>();

            var result = await sut.ImportAsync("");

            Assert.Multiple(() =>
            {
                Assert.That(result.Unsuccess, Has.Count.EqualTo(1));
                Assert.That(result.Success, Is.EqualTo(0));
                Assert.That(context.Resolve<NotebookStoreContext.NotebookStoreContext>().Notebooks, Is.Empty);
                Assert.That(result.Unsuccess.First().ErrorMessage, Is.EqualTo("No notebooks found in the file"), result.Unsuccess.FirstOrDefault()?.ErrorMessage);
            });
        }
    }
}
