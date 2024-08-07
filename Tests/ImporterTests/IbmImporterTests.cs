using IbmImporter;

namespace ImporterTests;

public class IbmImporterTests
{
    [Test]
    public async Task DataImporter_Import_Returns2SuccessAnd1Unsuccess()
    {
        using var context = TestStartup.CreateComponentsContext();

        var sut = context.Resolve<DataImporter>();

        var result = await sut.ImportAsync("test.json");

        Assert.Multiple(() =>
        {
            Assert.That(result.Success, Is.EqualTo(2));
            Assert.That(result.Unsuccess, Has.Count.EqualTo(1));
            Assert.That(result.Unsuccess[0].Index, Is.EqualTo(1));
        });
    }

    [Test]
    public void Import_FileDoesNotExist()
    {
        using var context = TestStartup.CreateComponentsContext();

        var sut = context.Resolve<DataImporter>();

        Assert.CatchAsync(async () => await sut.ImportAsync("abc.json"));
    }
}
