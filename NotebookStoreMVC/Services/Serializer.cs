using System.Text;
using System.Text.Json;
using System.Xml.Serialization;

namespace NotebookStoreMVC.Services;

public class Serializer<T> : ISerializer<T>
{
  public IEnumerable<T> JsonDeserialize(string json)
  {
    using var stream = new MemoryStream(Encoding.UTF8.GetBytes(json));
    var deserializedData = JsonSerializer.Deserialize<IEnumerable<T>>(stream);
    return deserializedData?.Cast<T>() ?? Enumerable.Empty<T>();
  }

  public string JsonSerialize(IEnumerable<T> data)
  {
    return JsonSerializer.Serialize(data);
  }

  public IEnumerable<T> XmlDeserialize(string xml)
  {
    using var stream = new MemoryStream(Encoding.UTF8.GetBytes(xml));
    var serializer = new XmlSerializer(typeof(IEnumerable<T>));
    var deserializedData = (IEnumerable<T>)serializer.Deserialize(stream)!;
    return deserializedData?.Cast<T>() ?? Enumerable.Empty<T>();
  }

  public string XmlSerialize(IEnumerable<T> data)
  {
    var serializer = new XmlSerializer(typeof(IEnumerable<T>));
    using var stream = new MemoryStream();
    serializer.Serialize(stream, data);
    stream.Position = 0;
    using var reader = new StreamReader(stream);
    return reader.ReadToEnd();
  }
}
