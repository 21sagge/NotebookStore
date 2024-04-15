using System.Text;
using Microsoft.AspNetCore.Mvc;
using NotebookStoreMVC.Models;
using NotebookStoreMVC.Repositories;
using NotebookStoreMVC.Services;

namespace NotebookStoreMVC.Controllers;

public class ExportController : Controller
{
	private readonly ISerializer _jsonSerializer;
	private readonly ISerializer _xmlSerializer;
	private readonly IRepository<BrandViewModel> _brandRepository;
	private readonly IRepository<CpuViewModel> _cpuRepository;
	private readonly IRepository<DisplayViewModel> _displayRepository;
	private readonly IRepository<MemoryViewModel> _memoryRepository;
	private readonly IRepository<ModelViewModel> _modelRepository;
	private readonly IRepository<StorageViewModel> _storageRepository;
	private readonly IRepository<NotebookViewModel> _notebookRepository;

	public ExportController(
		IRepository<BrandViewModel> brandRepository,
		IRepository<CpuViewModel> cpuRepository,
		IRepository<DisplayViewModel> displayRepository,
		IRepository<MemoryViewModel> memoryRepository,
		IRepository<ModelViewModel> modelRepository,
		IRepository<StorageViewModel> storageRepository,
		IRepository<NotebookViewModel> notebookRepository,
		[FromKeyedServices("json")] ISerializer jsonSerializer,
		[FromKeyedServices("xml")] ISerializer xmlSerializer)
	{
		_brandRepository = brandRepository;
		_cpuRepository = cpuRepository;
		_displayRepository = displayRepository;
		_memoryRepository = memoryRepository;
		_modelRepository = modelRepository;
		_storageRepository = storageRepository;
		_notebookRepository = notebookRepository;
		_jsonSerializer = jsonSerializer;
		_xmlSerializer = xmlSerializer;
	}

	// GET: Export
	[HttpGet]
	public IActionResult Index()
	{
		return View();
	}

	// POST: Export
	[HttpPost]
	public async Task<IActionResult> Export(string dataType, string format)
	{
		switch (dataType)
		{
			case "Brand":
				if (format != "json" && format != "xml")
				{
					return BadRequest("Format not supported");
				}

				var serializedData = (format == "json") ?
				_jsonSerializer.Serialize(await _brandRepository.Read()) :
				_xmlSerializer.Serialize(await _brandRepository.Read());

				var bytes = Encoding.UTF8.GetBytes(serializedData);

				return File(bytes, "application/octet-stream", $"{dataType.ToLower()}Export.{format}");

			case "Cpu":
				if (format != "json" && format != "xml")
				{
					return BadRequest("Format not supported");
				}

				serializedData = (format == "json") ?
				_jsonSerializer.Serialize(await _cpuRepository.Read()) :
				_xmlSerializer.Serialize(await _cpuRepository.Read());

				bytes = Encoding.UTF8.GetBytes(serializedData);

				return File(bytes, "application/octet-stream", $"{dataType.ToLower()}Export.{format}");

			case "Display":
				if (format != "json" && format != "xml")
				{
					return BadRequest("Format not supported");
				}

				serializedData = (format == "json") ?
				_jsonSerializer.Serialize(await _displayRepository.Read()) :
				_xmlSerializer.Serialize(await _displayRepository.Read());

				bytes = Encoding.UTF8.GetBytes(serializedData);

				return File(bytes, "application/octet-stream", $"{dataType.ToLower()}Export.{format}");

			case "Memory":
				if (format != "json" && format != "xml")
				{
					return BadRequest("Format not supported");
				}

				serializedData = (format == "json") ?
				_jsonSerializer.Serialize(await _memoryRepository.Read()) :
				_xmlSerializer.Serialize(await _memoryRepository.Read());

				bytes = Encoding.UTF8.GetBytes(serializedData);

				return File(bytes, "application/octet-stream", $"{dataType.ToLower()}Export.{format}");

			case "Model":
				if (format != "json" && format != "xml")
				{
					return BadRequest("Format not supported");
				}

				serializedData = (format == "json") ?
				_jsonSerializer.Serialize(await _modelRepository.Read()) :
				_xmlSerializer.Serialize(await _modelRepository.Read());

				bytes = Encoding.UTF8.GetBytes(serializedData);

				return File(bytes, "application/octet-stream", $"{dataType.ToLower()}Export.{format}");

			case "Storage":
				if (format != "json" && format != "xml")
				{
					return BadRequest("Format not supported");
				}

				serializedData = (format == "json") ?
				_jsonSerializer.Serialize(await _storageRepository.Read()) :
				_xmlSerializer.Serialize(await _storageRepository.Read());

				bytes = Encoding.UTF8.GetBytes(serializedData);

				return File(bytes, "application/octet-stream", $"{dataType.ToLower()}Export.{format}");

			case "Notebook":
				if (format != "json" && format != "xml")
				{
					return BadRequest("Format not supported");
				}

				serializedData = (format == "json") ?
				_jsonSerializer.Serialize(await _notebookRepository.Read()) :
				_xmlSerializer.Serialize(await _notebookRepository.Read());

				bytes = Encoding.UTF8.GetBytes(serializedData);

				return File(bytes, "application/octet-stream", $"{dataType.ToLower()}Export.{format}");

			default:
				return BadRequest("Data type not supported");
		}
	}
}
