using IbmImporter;
using IbmImporter.Models;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Monitor = IbmImporter.Models.Monitor;

namespace ImporterTests;

public class JsonFileParserTests
{
    [Test]
    public void Parse_WithValidData_ShouldReturnNotebookData()
    {
        using var context = TestStartup.CreateComponentsContext();

        var sut = context.Resolve<IJsonFileParser>();

        var result = sut.Parse("_data/test.json");

        Assert.Multiple(() =>
        {
            Assert.That(result.Customer, Is.EqualTo("Customer"));
            Assert.That(result.Notebooks, Has.Count.EqualTo(3));
            Assert.That(result.Notebooks.First().Name, Is.EqualTo("Name"));
        });
    }

    [Test]
    public void Parse_WithEmptyPathString_ShouldThrowException()
    {
        using var context = TestStartup.CreateComponentsContext();

        var sut = context.Resolve<IJsonFileParser>();

        Assert.Throws<InvalidOperationException>(() => sut.Parse(string.Empty));
    }

    [Test]
    public void Parse_WithFileNotFound_ShouldThrowException()
    {
        using var context = TestStartup.CreateComponentsContext();

        var sut = context.Resolve<IJsonFileParser>();

        Assert.Throws<InvalidOperationException>(() => sut.Parse("invalid.json"));
    }

    [Test]
    public void Parse_WithEmptyFile_ShouldReturnEmptyNotebookData()
    {
        using var context = TestStartup.CreateComponentsContext();

        var sut = context.Resolve<IJsonFileParser>();

        var result = sut.Parse("_data/empty.json");

        Assert.Multiple(() =>
        {
            Assert.That(result.Customer, Is.Empty);
            Assert.That(result.Notebooks, Is.Empty);
        });
    }

    [Test]
    public void Parse_FileIsNotJson_ShouldThrowException()
    {
        using var context = TestStartup.CreateComponentsContext();

        var sut = context.Resolve<IJsonFileParser>();

        Assert.Throws<InvalidOperationException>(() => sut.Parse("_data/pippo.txt"));
    }
}
