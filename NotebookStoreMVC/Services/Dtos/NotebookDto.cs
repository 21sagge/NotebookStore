using NotebookStoreMVC.Models;

namespace NotebookStoreMVC.Services;

public class NotebookDto
{
  public required string Color { get; set; }
  public required int Price { get; set; }
  public required int BrandId { get; set; }
  public required BrandViewModel Brand { get; set; }
  public required int ModelId { get; set; }
  public required ModelViewModel Model { get; set; }
  public required int CpuId { get; set; }
  public required CpuViewModel Cpu { get; set; }
  public required int DisplayId { get; set; }
  public required DisplayViewModel Display { get; set; }
  public required int MemoryId { get; set; }
  public required MemoryViewModel Memory { get; set; }
  public required int StorageId { get; set; }
  public required StorageViewModel Storage { get; set; }
}
