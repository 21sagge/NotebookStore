using System.Text;
using System.Xml.Serialization;
using NotebookStoreMVC.Models;
using NotebookStoreMVC.Services;

namespace NotebookStoreMVC;

public class XmlHandler : ISerializer
{
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
		switch (graph)
		{
			case IEnumerable<CpuViewModel> cpuData:
				return Serialize(cpuData.Select(cpu => new CpuDto
				{
					Id = cpu.Id,
					Brand = cpu.Brand!,
					Model = cpu.Model!
				}));

			case IEnumerable<DisplayViewModel> displayData:
				return Serialize(displayData.Select(display => new DisplayDto
				{
					Id = display.Id,
					Size = display.Size,
					ResolutionWidth = display.ResolutionWidth,
					ResolutionHeight = display.ResolutionHeight
				}));

			case IEnumerable<MemoryViewModel> memoryData:
				return Serialize(memoryData.Select(memory => new MemoryDto
				{
					Id = memory.Id,
					Capacity = memory.Capacity,
					Speed = memory.Speed
				}));

			case IEnumerable<StorageViewModel> storageData:
				return Serialize(storageData.Select(storage => new StorageDto
				{
					Id = storage.Id,
					Capacity = storage.Capacity,
					Type = storage.Type
				}));

			case IEnumerable<BrandViewModel> brandData:
				return Serialize(brandData.Select(brand => new BrandDto
				{
					Id = brand.Id,
					Name = brand.Name!
				}));

			case IEnumerable<ModelViewModel> modelData:
				return Serialize(modelData.Select(model => new ModelDto
				{
					Id = model.Id,
					Name = model.Name
				}));

			case IEnumerable<NotebookViewModel> notebookData:
				return Serialize(notebookData.Select(notebook => new NotebookDto
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
				}));

			default:
				throw new InvalidOperationException($"Unsupported type: {graph?.GetType()}");
		}

		static string Serialize<T>(IEnumerable<T> data)
		{
			using var stream = new MemoryStream();
			new XmlSerializer(typeof(List<T>)).Serialize(stream, data.ToList());

			return Encoding.UTF8.GetString(stream.ToArray());
		}
	}
}
