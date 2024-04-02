namespace NotebookStore.Models
{
  public class Notebook
  {
    public int Id { get; set; }
    public required string Color { get; set; }
    public int Price { get; set; }
    public int BrandId { get; set; }
    public required Brand Brand { get; set; }
    public int ModelId { get; set; }
    public required Model Model { get; set; }
    public int CpuId { get; set; }
    public required Cpu Cpu { get; set; }
    public int DisplayId { get; set; }
    public required Display Display { get; set; }
    public int MemoryId { get; set; }
    public required Memory Memory { get; set; }
    public int StorageId { get; set; }
    public required Storage Storage { get; set; }
  }
}