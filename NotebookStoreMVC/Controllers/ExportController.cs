using System.Text;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using NotebookStore.Business;

namespace NotebookStoreMVC.Controllers;

public class ExportController : Controller
{
    private readonly BrandService _brandService;
    private readonly CpuService _cpuService;
    private readonly DisplayService _displayService;
    private readonly MemoryService _memoryService;
    private readonly ModelService _modelService;
    private readonly StorageService _storageService;
    private readonly NotebookService _notebookService;
    private readonly IEnumerable<ISerializer> serializers;
    private readonly IMapper mapper;

    public ExportController(
        BrandService brandService,
        CpuService cpuService,
        DisplayService displayService,
        MemoryService memoryService,
        ModelService modelService,
        StorageService storageService,
        NotebookService notebookService,
        IEnumerable<ISerializer> serializer,
        IMapper mapper)
    {
        _brandService = brandService;
        _cpuService = cpuService;
        _displayService = displayService;
        _memoryService = memoryService;
        _modelService = modelService;
        _storageService = storageService;
        _notebookService = notebookService;
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
                var brands = (await _brandService.GetBrands()).ToList();

                var brandsMapped = mapper.Map<IEnumerable<BrandDto>>(brands).ToList();

                var serializedData = serializer.Serialize(brandsMapped);

                var bytes = Encoding.UTF8.GetBytes(serializedData);

                return File(bytes, "application/octet-stream", $"{dataType.ToLower()}Export.{format}");

            case "Cpu":
                var cpus = (await _cpuService.GetCpus()).ToList();

                var cpusMapped = mapper.Map<IEnumerable<CpuDto>>(cpus).ToList();

                serializedData = serializer.Serialize(cpusMapped);

                bytes = Encoding.UTF8.GetBytes(serializedData);

                return File(bytes, "application/octet-stream", $"{dataType.ToLower()}Export.{format}");

            case "Display":
                var displays = (await _displayService.GetDisplays()).ToList();

                var displaysMapped = mapper.Map<IEnumerable<DisplayDto>>(displays).ToList();

                serializedData = serializer.Serialize(displaysMapped);

                bytes = Encoding.UTF8.GetBytes(serializedData);

                return File(bytes, "application/octet-stream", $"{dataType.ToLower()}Export.{format}");

            case "Memory":
                var memories = (await _memoryService.GetMemories()).ToList();

                var memoriesMapped = mapper.Map<IEnumerable<MemoryDto>>(memories).ToList();

                serializedData = serializer.Serialize(memoriesMapped);

                bytes = Encoding.UTF8.GetBytes(serializedData);

                return File(bytes, "application/octet-stream", $"{dataType.ToLower()}Export.{format}");

            case "Model":
                var models = (await _modelService.GetModels()).ToList();

                var modelsMapped = mapper.Map<IEnumerable<ModelDto>>(models).ToList();

                serializedData = serializer.Serialize(modelsMapped);

                bytes = Encoding.UTF8.GetBytes(serializedData);

                return File(bytes, "application/octet-stream", $"{dataType.ToLower()}Export.{format}");

            case "Storage":
                var storages = (await _storageService.GetStorages()).ToList();

                var storagesMapped = mapper.Map<IEnumerable<StorageDto>>(storages).ToList();

                serializedData = serializer.Serialize(storagesMapped);

                bytes = Encoding.UTF8.GetBytes(serializedData);

                return File(bytes, "application/octet-stream", $"{dataType.ToLower()}Export.{format}");

            case "Notebook":
                var notebooks = (await _notebookService.GetNotebooks()).ToList();

                var notebooksMapped = mapper.Map<IEnumerable<NotebookDto>>(notebooks).ToList();

                serializedData = serializer.Serialize(notebooksMapped);

                bytes = Encoding.UTF8.GetBytes(serializedData);

                return File(bytes, "application/octet-stream", $"{dataType.ToLower()}Export.{format}");

            default:
                return BadRequest("Data type not supported");
        }
    }
}
