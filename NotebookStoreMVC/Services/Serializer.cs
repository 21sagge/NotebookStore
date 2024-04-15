namespace NotebookStoreMVC.Services;

using System.Text;
using System.Text.Json;
using System.Xml.Serialization;
using NotebookStoreMVC.Models;

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
				case CpuViewModel cpu:
					return new CpuDto
					{
						Id = cpu.Id,
						Brand = cpu.Brand!,
						Model = cpu.Model!
					} as BaseDto;
				case DisplayViewModel display:
					return new DisplayDto
					{
						Id = display.Id,
						Size = display.Size,
						ResolutionWidth = display.ResolutionWidth,
						ResolutionHeight = display.ResolutionHeight
					} as BaseDto;
				case MemoryViewModel memory:
					return new MemoryDto
					{
						Id = memory.Id,
						Capacity = memory.Capacity,
						Speed = memory.Speed
					} as BaseDto;
				case StorageViewModel storage:
					return new StorageDto
					{
						Id = storage.Id,
						Capacity = storage.Capacity,
						Type = storage.Type
					} as BaseDto;
				case BrandViewModel brand:
					return new BrandDto
					{
						Id = brand.Id,
						Name = brand.Name!
					} as BaseDto;
				case ModelViewModel model:
					return new ModelDto
					{
						Id = model.Id,
						Name = model.Name
					} as BaseDto;
				case NotebookViewModel notebook:
					return new NotebookDto
					{
						Id = notebook.Id,
						Brand = notebook.Brand!,
						Model = notebook.Model!,
						Cpu = notebook.Cpu!,
						Display = notebook.Display!,
						Memory = notebook.Memory!,
						Storage = notebook.Storage!,
						Color = notebook.Color!,
						Price = notebook.Price,
						BrandId = notebook.BrandId,
						ModelId = notebook.ModelId,
						CpuId = notebook.CpuId,
						DisplayId = notebook.DisplayId,
						MemoryId = notebook.MemoryId,
						StorageId = notebook.StorageId
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
