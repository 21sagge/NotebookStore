namespace NotebookStore.Business;

public class BrandDto
{
	public int Id { get; set; }
	public required string Name { get; set; }
	public string? CreatedBy { get; set; }
	public required string CreatedAt { get; set; }
}
