using System.Text;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using NotebookStore.Business;

namespace NotebookStoreMVC.Controllers;

public class ExportController : Controller
{
    private readonly IServices services;
    private readonly IEnumerable<ISerializer> serializers;
    private readonly IMapper mapper;

    public ExportController(
        IServices services,
        IEnumerable<ISerializer> serializer,
        IMapper mapper)
    {
        this.services = services;
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
                var brands = (await services.Brands.GetAll()).ToList();

                var brandsMapped = mapper.Map<IEnumerable<BrandDto>>(brands).ToList();

                var serializedData = serializer.Serialize(brandsMapped);

                var bytes = Encoding.UTF8.GetBytes(serializedData);

                return File(bytes, "application/octet-stream", $"{dataType.ToLower()}Export.{format}");

            case "Cpu":
                var cpus = (await services.Cpus.GetAll()).ToList();

                var cpusMapped = mapper.Map<IEnumerable<CpuDto>>(cpus).ToList();

                serializedData = serializer.Serialize(cpusMapped);

                bytes = Encoding.UTF8.GetBytes(serializedData);

                return File(bytes, "application/octet-stream", $"{dataType.ToLower()}Export.{format}");

            case "Display":
                var displays = (await services.Displays.GetAll()).ToList();

                var displaysMapped = mapper.Map<IEnumerable<DisplayDto>>(displays).ToList();

                serializedData = serializer.Serialize(displaysMapped);

                bytes = Encoding.UTF8.GetBytes(serializedData);

                return File(bytes, "application/octet-stream", $"{dataType.ToLower()}Export.{format}");

            case "Memory":
                var memories = (await services.Memories.GetAll()).ToList();

                var memoriesMapped = mapper.Map<IEnumerable<MemoryDto>>(memories).ToList();

                serializedData = serializer.Serialize(memoriesMapped);

                bytes = Encoding.UTF8.GetBytes(serializedData);

                return File(bytes, "application/octet-stream", $"{dataType.ToLower()}Export.{format}");

            case "Model":
                var models = (await services.Models.GetAll()).ToList();

                var modelsMapped = mapper.Map<IEnumerable<ModelDto>>(models).ToList();

                serializedData = serializer.Serialize(modelsMapped);

                bytes = Encoding.UTF8.GetBytes(serializedData);

                return File(bytes, "application/octet-stream", $"{dataType.ToLower()}Export.{format}");

            case "Storage":
                var storages = (await services.Storages.GetAll()).ToList();

                var storagesMapped = mapper.Map<IEnumerable<StorageDto>>(storages).ToList();

                serializedData = serializer.Serialize(storagesMapped);

                bytes = Encoding.UTF8.GetBytes(serializedData);

                return File(bytes, "application/octet-stream", $"{dataType.ToLower()}Export.{format}");

            case "Notebook":
                var notebooks = (await services.Notebooks.GetAll()).ToList();

                var notebooksMapped = mapper.Map<IEnumerable<NotebookDto>>(notebooks).ToList();

                serializedData = serializer.Serialize(notebooksMapped);

                bytes = Encoding.UTF8.GetBytes(serializedData);

                return File(bytes, "application/octet-stream", $"{dataType.ToLower()}Export.{format}");

            default:
                return BadRequest("Data type not supported");
        }
    }
}
