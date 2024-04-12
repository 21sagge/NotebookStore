namespace NotebookStoreMVC.Controllers;

using System.Text;
using Microsoft.AspNetCore.Mvc;
using NotebookStoreMVC.Models;
using NotebookStoreMVC.Repositories;
using NotebookStoreMVC.Services;

public class ExportController : Controller
{
  private readonly ISerializer<CpuViewModel> _cpuSerializer;
  private readonly ISerializer<DisplayViewModel> _displaySerializer;
  private readonly IRepository<CpuViewModel> _cpuRepository;
  private readonly IRepository<DisplayViewModel> _displayRepository;

  public ExportController(ISerializer<CpuViewModel> cpuSerializer, ISerializer<DisplayViewModel> displaySerializer, IRepository<CpuViewModel> cpuRepository, IRepository<DisplayViewModel> displayRepository)
  {
    _cpuSerializer = cpuSerializer;
    _displaySerializer = displaySerializer;
    _cpuRepository = cpuRepository;
    _displayRepository = displayRepository;
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
    if (dataType == "CpuViewModel")
    {
      // Ottieni i dati da esportare
      var data = await _cpuRepository.Read();

      // Serializza i dati nel formato selezionato
      string serializedData;
      if (format == "json")
      {
        serializedData = _cpuSerializer.JsonSerialize(data);
      }
      else if (format == "xml")
      {
        serializedData = _cpuSerializer.XmlSerialize(data);
      }
      else
      {
        return BadRequest("Format not supported");
      }

      // Restituisci i dati serializzati come file da scaricare
      var bytes = Encoding.UTF8.GetBytes(serializedData);
      return File(bytes, "application/octet-stream", $"{dataType.ToLower()}Export.{format}");
    }
    else if (dataType == "DisplayViewModel")
    {
      // Ottieni i dati da esportare
      var data = await _displayRepository.Read();

      // Serializza i dati nel formato selezionato
      string serializedData;
      if (format == "json")
      {
        serializedData = _displaySerializer.JsonSerialize(data);
      }
      else if (format == "xml")
      {
        serializedData = _displaySerializer.XmlSerialize(data);
      }
      else
      {
        return BadRequest("Format not supported");
      }

      // Restituisci i dati serializzati come file da scaricare
      var bytes = Encoding.UTF8.GetBytes(serializedData);
      return File(bytes, "application/octet-stream", $"{dataType.ToLower()}Export.{format}");
    }
    else
    {
      return BadRequest("Data type not supported");
    }
  }
}
