using System.Text;
using Microsoft.AspNetCore.Mvc;
using NotebookStore.DAL;
using NotebookStoreMVC.Services;
using NotebookStore.Entities;
using AutoMapper;

namespace NotebookStoreMVC.Controllers;

public class ExportController : Controller
{
    private readonly IRepository<Brand> _brandRepository;
    private readonly IRepository<Cpu> _cpuRepository;
    private readonly IRepository<Display> _displayRepository;
    private readonly IRepository<Memory> _memoryRepository;
    private readonly IRepository<Model> _modelRepository;
    private readonly IRepository<Storage> _storageRepository;
    private readonly IRepository<Notebook> _notebookRepository;
    private readonly IEnumerable<ISerializer> serializers;
    private readonly IMapper mapper;

    public ExportController(
        IRepository<Brand> brandRepository,
        IRepository<Cpu> cpuRepository,
        IRepository<Display> displayRepository,
        IRepository<Memory> memoryRepository,
        IRepository<Model> modelRepository,
        IRepository<Storage> storageRepository,
        IRepository<Notebook> notebookRepository,
        IEnumerable<ISerializer> serializer,
        IMapper mapper)
    {
        _brandRepository = brandRepository;
        _cpuRepository = cpuRepository;
        _displayRepository = displayRepository;
        _memoryRepository = memoryRepository;
        _modelRepository = modelRepository;
        _storageRepository = storageRepository;
        _notebookRepository = notebookRepository;
        serializers = serializer;
        this.mapper = mapper;
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
        var serializer = serializers.FirstOrDefault(x => x.Format == format);

        if (serializer == null)
        {
            return BadRequest("Format not supported");
        }

        switch (dataType)
        {
            case "Brand":
                var brands = (await _brandRepository.Read()).ToList();

                var brandsMapped = mapper.Map<IEnumerable<BrandDto>>(brands).ToList();

                var serializedData = serializer.Serialize(brandsMapped);

                var bytes = Encoding.UTF8.GetBytes(serializedData);

                return File(bytes, "application/octet-stream", $"{dataType.ToLower()}Export.{format}");

            case "Cpu":
                var cpus = (await _cpuRepository.Read()).ToList();

                var cpusMapped = mapper.Map<IEnumerable<CpuDto>>(cpus).ToList();

                serializedData = serializer.Serialize(cpusMapped);

                bytes = Encoding.UTF8.GetBytes(serializedData);

                return File(bytes, "application/octet-stream", $"{dataType.ToLower()}Export.{format}");

            case "Display":
                var displays = (await _displayRepository.Read()).ToList();

                var displaysMapped = mapper.Map<IEnumerable<DisplayDto>>(displays).ToList();

                serializedData = serializer.Serialize(displaysMapped);

                bytes = Encoding.UTF8.GetBytes(serializedData);

                return File(bytes, "application/octet-stream", $"{dataType.ToLower()}Export.{format}");

            case "Memory":
                var memories = (await _memoryRepository.Read()).ToList();

                var memoriesMapped = mapper.Map<IEnumerable<MemoryDto>>(memories).ToList();

                serializedData = serializer.Serialize(memoriesMapped);

                bytes = Encoding.UTF8.GetBytes(serializedData);

                return File(bytes, "application/octet-stream", $"{dataType.ToLower()}Export.{format}");

            case "Model":
                var models = (await _modelRepository.Read()).ToList();

                var modelsMapped = mapper.Map<IEnumerable<ModelDto>>(models).ToList();

                serializedData = serializer.Serialize(modelsMapped);

                bytes = Encoding.UTF8.GetBytes(serializedData);

                return File(bytes, "application/octet-stream", $"{dataType.ToLower()}Export.{format}");

            case "Storage":
                var storages = (await _storageRepository.Read()).ToList();

                var storagesMapped = mapper.Map<IEnumerable<StorageDto>>(storages).ToList();

                serializedData = serializer.Serialize(storagesMapped);

                bytes = Encoding.UTF8.GetBytes(serializedData);

                return File(bytes, "application/octet-stream", $"{dataType.ToLower()}Export.{format}");

            case "Notebook":
                var notebooks = (await _notebookRepository.Read()).ToList();

                var notebooksMapped = mapper.Map<IEnumerable<NotebookDto>>(notebooks).ToList();

                serializedData = serializer.Serialize(notebooksMapped);

                bytes = Encoding.UTF8.GetBytes(serializedData);

                return File(bytes, "application/octet-stream", $"{dataType.ToLower()}Export.{format}");

            default:
                return BadRequest("Data type not supported");
        }
    }
}
