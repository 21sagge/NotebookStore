using System.ComponentModel.DataAnnotations;

namespace NotebookStore.Business;

public class ModelDto
{
	public int Id { get; set; }
	[MaxLength(50)]
	public required string Name { get; set; }
	public string? CreatedBy { get; set; }
	public required string CreatedAt { get; set; }
}
