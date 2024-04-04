namespace NotebookStore.Entities;

public class Notebook
{
  public int Id { get; set; }
  public required string Color { get; set; }
  public int Price { get; set; }
  public int BrandId { get; set; }
  public virtual required Brand Brand { get; set; }
  public int ModelId { get; set; }
  public virtual required Model Model { get; set; }
  public int CpuId { get; set; }
  public virtual required Cpu Cpu { get; set; }
  public int DisplayId { get; set; }
  public virtual required Display Display { get; set; }
  public int MemoryId { get; set; }
  public virtual required Memory Memory { get; set; }
  public int StorageId { get; set; }
  public virtual required Storage Storage { get; set; }
}