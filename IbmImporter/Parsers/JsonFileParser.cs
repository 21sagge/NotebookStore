using IbmImporter.Models;
using NotebookStore.Business;

namespace IbmImporter;

public class JsonFileParser : IJsonFileParser
{
    private readonly ISerializer serializer;

    public JsonFileParser(IEnumerable<ISerializer> serializers)
    {
        serializer = serializers.FirstOrDefault(s => s.Format == "json") ?? throw new ArgumentException("No json serializer found");
    }

    public NotebookData? Parse(string path)
    {
        try
        {
            var jsonString = File.ReadAllText(path);

            return serializer.Deserialize<NotebookData>(jsonString);
        }
        catch (FileNotFoundException)
        {
            throw new InvalidOperationException("File not found");
        }
        catch (Exception)
        {
            throw new InvalidOperationException("An error occurred while parsing the file");
        }
    }
}
