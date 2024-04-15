using System.Text;
using Microsoft.AspNetCore.Mvc;
using NotebookStoreMVC.Models;
using NotebookStoreMVC.Repositories;
using NotebookStoreMVC.Services;

namespace NotebookStoreMVC.Controllers;

public class ExportController : Controller
{
	private readonly ISerializer<BrandViewModel> _brandSerializer;
	private readonly ISerializer<CpuViewModel> _cpuSerializer;
	private readonly ISerializer<DisplayViewModel> _displaySerializer;
	private readonly ISerializer<MemoryViewModel> _memorySerializer;
	private readonly ISerializer<ModelViewModel> _modelSerializer;
	private readonly ISerializer<StorageViewModel> _storageSerializer;
	private readonly ISerializer<NotebookViewModel> _notebookSerializer;
	private readonly IRepository<BrandViewModel> _brandRepository;
	private readonly IRepository<CpuViewModel> _cpuRepository;
	private readonly IRepository<DisplayViewModel> _displayRepository;
	private readonly IRepository<MemoryViewModel> _memoryRepository;
	private readonly IRepository<ModelViewModel> _modelRepository;
	private readonly IRepository<StorageViewModel> _storageRepository;
	private readonly INotebookRepository _notebookRepository;

	public ExportController(ISerializer<CpuViewModel> cpuSerializer,
							ISerializer<DisplayViewModel> displaySerializer,
							ISerializer<MemoryViewModel> memorySerializer,
							ISerializer<BrandViewModel> brandSerializer,
							ISerializer<ModelViewModel> modelSerializer,
							ISerializer<StorageViewModel> storageSerializer,
							ISerializer<NotebookViewModel> notebookSerializer,
							IRepository<CpuViewModel> cpuRepository,
							IRepository<DisplayViewModel> displayRepository,
							IRepository<MemoryViewModel> memoryRepository,
							IRepository<BrandViewModel> brandRepository,
							IRepository<ModelViewModel> modelRepository,
							IRepository<StorageViewModel> storageRepository,
							INotebookRepository notebookRepository)
	{
		_cpuSerializer = cpuSerializer;
		_displaySerializer = displaySerializer;
		_memorySerializer = memorySerializer;
		_brandSerializer = brandSerializer;
		_modelSerializer = modelSerializer;
		_storageSerializer = storageSerializer;
		_notebookSerializer = notebookSerializer;
		_brandRepository = brandRepository;
		_cpuRepository = cpuRepository;
		_displayRepository = displayRepository;
		_memoryRepository = memoryRepository;
		_modelRepository = modelRepository;
		_storageRepository = storageRepository;
		_notebookRepository = notebookRepository;
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
					_brandSerializer.JsonSerialize(await _brandRepository.Read()) :
					_brandSerializer.XmlSerialize(await _brandRepository.Read());
				var bytes = Encoding.UTF8.GetBytes(serializedData);
				return File(bytes, "application/octet-stream", $"{dataType.ToLower()}Export.{format}");
			case "Cpu":
				if (format != "json" && format != "xml")
				{
					return BadRequest("Format not supported");
				}
				serializedData = (format == "json") ?
				_cpuSerializer.JsonSerialize(await _cpuRepository.Read()) :
				_cpuSerializer.XmlSerialize(await _cpuRepository.Read());
				bytes = Encoding.UTF8.GetBytes(serializedData);
				return File(bytes, "application/octet-stream", $"{dataType.ToLower()}Export.{format}");
			case "Display":
				if (format != "json" && format != "xml")
				{
					return BadRequest("Format not supported");
				}
				serializedData = (format == "json") ?
				_displaySerializer.JsonSerialize(await _displayRepository.Read()) :
				_displaySerializer.XmlSerialize(await _displayRepository.Read());
				bytes = Encoding.UTF8.GetBytes(serializedData);
				return File(bytes, "application/octet-stream", $"{dataType.ToLower()}Export.{format}");
			case "Memory":
				if (format != "json" && format != "xml")
				{
					return BadRequest("Format not supported");
				}
				serializedData = (format == "json") ?
				_memorySerializer.JsonSerialize(await _memoryRepository.Read()) :
				_memorySerializer.XmlSerialize(await _memoryRepository.Read());
				bytes = Encoding.UTF8.GetBytes(serializedData);
				return File(bytes, "application/octet-stream", $"{dataType.ToLower()}Export.{format}");
			case "Model":
				if (format != "json" && format != "xml")
				{
					return BadRequest("Format not supported");
				}
				serializedData = (format == "json") ?
				_modelSerializer.JsonSerialize(await _modelRepository.Read()) :
				_modelSerializer.XmlSerialize(await _modelRepository.Read());
				bytes = Encoding.UTF8.GetBytes(serializedData);
				return File(bytes, "application/octet-stream", $"{dataType.ToLower()}Export.{format}");
			case "Storage":
				if (format != "json" && format != "xml")
				{
					return BadRequest("Format not supported");
				}
				serializedData = (format == "json") ?
				_storageSerializer.JsonSerialize(await _storageRepository.Read()) :
				_storageSerializer.XmlSerialize(await _storageRepository.Read());
				bytes = Encoding.UTF8.GetBytes(serializedData);
				return File(bytes, "application/octet-stream", $"{dataType.ToLower()}Export.{format}");
			case "Notebook":
				if (format != "json" && format != "xml")
				{
					return BadRequest("Format not supported");
				}
				serializedData = (format == "json") ?
				_notebookSerializer.JsonSerialize(await _notebookRepository.Read()) :
				_notebookSerializer.XmlSerialize(await _notebookRepository.Read());
				bytes = Encoding.UTF8.GetBytes(serializedData);
				return File(bytes, "application/octet-stream", $"{dataType.ToLower()}Export.{format}");
			default:
				return BadRequest("Data type not supported");
		}
	}
}
