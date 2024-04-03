namespace NotebookStore.Entities
{
  public class Notebook
  {
    public int Id { get; set; }
    public required string Color { get; set; }
    public int Price { get; set; }
    public int BrandId { get; set; }
    public int ModelId { get; set; }
    public int CpuId { get; set; }
    public int DisplayId { get; set; }
    public int MemoryId { get; set; }
    public int StorageId { get; set; }
  }
}