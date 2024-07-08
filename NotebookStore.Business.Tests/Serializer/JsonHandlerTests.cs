namespace NotebookStore.Business.Tests.Serializer;

[TestFixture()]
public class JsonHandlerTests
{
    public JsonHandlerTests()
    {
    }

    [Test]
    public void Serialize_ObjectIsNull_Works()
    {
        // Arrange
        var sut = new JsonHandler();

        // Act
        var result = sut.Serialize<Foo?>(null);

        // Assert
        Assert.That(result, Is.EqualTo("null"));
    }

    [Test]
    public void Deserialize_ObjectIsNull_Works()
    {
        // Arrange
        var sut = new JsonHandler();

        // Act
        var result = sut.Deserialize<Foo>("null");

        // Assert
        Assert.That(result, Is.Null);
    }

    [Test]
    public void Serialize_ObjectIsNotNull_Works()
    {
        // Arrange
        var sut = new JsonHandler();
        var foo = new Foo { Id = 1, Name = "Test" };

        // Act
        var result = sut.Serialize(foo);

        // Assert
        Assert.That(result, Is.EqualTo("{\"Id\":1,\"Name\":\"Test\"}"));
    }

    [Test]
    public void Deserialize_ObjectIsNotNull_Works()
    {
        // Arrange
        var sut = new JsonHandler();
        var json = "{\"Id\":1,\"Name\":\"Test\"}";

        // Act
        var result = sut.Deserialize<Foo>(json);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.Id, Is.EqualTo(1));
            Assert.That(result.Name, Is.EqualTo("Test"));
        });
    }

    private class Foo
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}
