namespace NotebookStoreMVC.Services;

using System.Text;
using System.Text.Json;
using System.Xml.Serialization;
using NotebookStore.Entities;

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
    var dtoData = data.Select(item =>
    {
      switch (item)
      {
        case Cpu cpu:
          return new CpuDto
          {
            Id = cpu.Id,
            Brand = cpu.Brand,
            Model = cpu.Model
          } as BaseDto;
        case Display display:
          return new DisplayDto
          {
            Id = display.Id,
            Size = display.Size,
            ResolutionWidth = display.ResolutionWidth,
            ResolutionHeight = display.ResolutionHeight
          } as BaseDto;
        default:
          throw new InvalidOperationException($"Unsupported type: {item?.GetType()}");
      }
    });

    var xmlSerializer = new XmlSerializer(typeof(List<BaseDto>));
    using var stream = new MemoryStream();
    xmlSerializer.Serialize(stream, dtoData.ToList());
    stream.Position = 0;
    using var reader = new StreamReader(stream);
    return reader.ReadToEnd();
  }
}
