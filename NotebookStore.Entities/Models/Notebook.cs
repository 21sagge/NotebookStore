using System.ComponentModel.DataAnnotations;

namespace NotebookStore.Entities;

public class Notebook : IAuditable
{
  public int Id { get; set; }
  [MaxLength(50)]
  public required string Color { get; set; }
  [Range(0, 10000)]
  public int Price { get; set; }
  public int BrandId { get; set; }
  public virtual Brand? Brand { get; set; }
  public int ModelId { get; set; }
  public virtual Model? Model { get; set; }
  public int CpuId { get; set; }
  public virtual Cpu? Cpu { get; set; }
  public int DisplayId { get; set; }
  public virtual Display? Display { get; set; }
  public int MemoryId { get; set; }
  public virtual Memory? Memory { get; set; }
  public int StorageId { get; set; }
  public virtual Storage? Storage { get; set; }
  public string? CreatedBy { get; set; }
  public required string CreatedAt { get; set; }
}