using System.Text;
using Microsoft.AspNetCore.Mvc;
using NotebookStoreMVC.Models;
using NotebookStore.Repositories;
using NotebookStoreMVC.Services;
using NotebookStore.Entities;

namespace NotebookStoreMVC.Controllers;

public class ExportController : Controller
{
	private readonly ISerializer _jsonSerializer;
	private readonly ISerializer _xmlSerializer;
	private readonly IRepository<Brand> _brandRepository;
	private readonly IRepository<Cpu> _cpuRepository;
	private readonly IRepository<Display> _displayRepository;
	private readonly IRepository<Memory> _memoryRepository;
	private readonly IRepository<Model> _modelRepository;
	private readonly IRepository<Storage> _storageRepository;
	private readonly IRepository<Notebook> _notebookRepository;

	public ExportController(
		IRepository<Brand> brandRepository,
		IRepository<Cpu> cpuRepository,
		IRepository<Display> displayRepository,
		IRepository<Memory> memoryRepository,
		IRepository<Model> modelRepository,
		IRepository<Storage> storageRepository,
		INotebookRepository notebookRepository,
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
		if (format != "json" && format != "xml")
		{
			return BadRequest("Format not supported");
		}

		switch (dataType)
		{
			case "Brand":
				var serializedData = (format == "json") ?
				_jsonSerializer.Serialize(await _brandRepository.Read()) :
				_xmlSerializer.Serialize(await _brandRepository.Read());

				var bytes = Encoding.UTF8.GetBytes(serializedData);

				return File(bytes, "application/octet-stream", $"{dataType.ToLower()}Export.{format}");

			case "Cpu":
				serializedData = (format == "json") ?
				_jsonSerializer.Serialize(await _cpuRepository.Read()) :
				_xmlSerializer.Serialize(await _cpuRepository.Read());

				bytes = Encoding.UTF8.GetBytes(serializedData);

				return File(bytes, "application/octet-stream", $"{dataType.ToLower()}Export.{format}");

			case "Display":
				serializedData = (format == "json") ?
				_jsonSerializer.Serialize(await _displayRepository.Read()) :
				_xmlSerializer.Serialize(await _displayRepository.Read());

				bytes = Encoding.UTF8.GetBytes(serializedData);

				return File(bytes, "application/octet-stream", $"{dataType.ToLower()}Export.{format}");

			case "Memory":
				serializedData = (format == "json") ?
				_jsonSerializer.Serialize(await _memoryRepository.Read()) :
				_xmlSerializer.Serialize(await _memoryRepository.Read());

				bytes = Encoding.UTF8.GetBytes(serializedData);

				return File(bytes, "application/octet-stream", $"{dataType.ToLower()}Export.{format}");

			case "Model":
				serializedData = (format == "json") ?
				_jsonSerializer.Serialize(await _modelRepository.Read()) :
				_xmlSerializer.Serialize(await _modelRepository.Read());

				bytes = Encoding.UTF8.GetBytes(serializedData);

				return File(bytes, "application/octet-stream", $"{dataType.ToLower()}Export.{format}");

			case "Storage":
				serializedData = (format == "json") ?
				_jsonSerializer.Serialize(await _storageRepository.Read()) :
				_xmlSerializer.Serialize(await _storageRepository.Read());

				bytes = Encoding.UTF8.GetBytes(serializedData);

				return File(bytes, "application/octet-stream", $"{dataType.ToLower()}Export.{format}");

			case "Notebook":
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
