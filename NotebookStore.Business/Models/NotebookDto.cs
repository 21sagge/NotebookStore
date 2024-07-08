namespace NotebookStore.Business;

public class NotebookDto : IAuditableDto
{
	public int Id { get; set; }
	public required string Color { get; set; }
	public required int Price { get; set; }
	public int BrandId { get; set; }
	public BrandDto? Brand { get; set; }
	public int ModelId { get; set; }
	public ModelDto? Model { get; set; }
	public int CpuId { get; set; }
	public CpuDto? Cpu { get; set; }
	public int DisplayId { get; set; }
	public DisplayDto? Display { get; set; }
	public int MemoryId { get; set; }
	public MemoryDto? Memory { get; set; }
	public int StorageId { get; set; }
	public StorageDto? Storage { get; set; }
	public bool CanUpdate { get; set; }
	public bool CanDelete { get; set; }
}
