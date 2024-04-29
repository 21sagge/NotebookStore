using System.Text;
using System.Xml.Serialization;

namespace NotebookStore.Business;

public class XmlHandler : ISerializer
{
    public string Format => "xml";

    public TDest? Deserialize<TDest>(string source)
    {
        if (string.IsNullOrEmpty(source))
        {
            return default;
        }

        using var stream = new MemoryStream(Encoding.UTF8.GetBytes(source));
        var deserializedData = new XmlSerializer(typeof(TDest)).Deserialize(stream);

        return (TDest)deserializedData!;
    }

    public string Serialize<TSource>(TSource graph)
    {
        using var stream = new MemoryStream();
        new XmlSerializer(typeof(TSource)).Serialize(stream, graph);

        return Encoding.UTF8.GetString(stream.ToArray());
    }
}
