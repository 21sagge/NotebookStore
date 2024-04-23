namespace NotebookStore.Business;

public class NotebookDto
{
	public int Id { get; set; }
	public required string Color { get; set; }
	public required int Price { get; set; }
	public required int BrandId { get; set; }
	public required BrandDto Brand { get; set; }
	public required int ModelId { get; set; }
	public required ModelDto Model { get; set; }
	public required int CpuId { get; set; }
	public required CpuDto Cpu { get; set; }
	public required int DisplayId { get; set; }
	public required DisplayDto Display { get; set; }
	public required int MemoryId { get; set; }
	public required MemoryDto Memory { get; set; }
	public required int StorageId { get; set; }
	public required StorageDto Storage { get; set; }
}
