﻿namespace NotebookStoreMVC.Controllers;

using System.Text;
using Microsoft.AspNetCore.Mvc;
using NotebookStore.Entities;
using NotebookStoreMVC.Repositories;
using NotebookStoreMVC.Services;

public class ExportController : Controller
{
  private readonly ISerializer<Cpu> _cpuSerializer;
  private readonly IRepository<Cpu> _cpuRepository;

  public ExportController(ISerializer<Cpu> cpuSerializer, IRepository<Cpu> cpuRepository)
  {
    _cpuSerializer = cpuSerializer;
    _cpuRepository = cpuRepository;
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
    if (dataType == "Cpu" || dataType == "Display")
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
    else
    {
      return BadRequest("Data type not supported");
    }
  }
}
