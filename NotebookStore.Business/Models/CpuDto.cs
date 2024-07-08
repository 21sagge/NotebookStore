using System.ComponentModel.DataAnnotations;

namespace NotebookStore.Business;

public class CpuDto : IAuditableDto
{
	public int Id { get; set; }
	[MaxLength(50)]
	public required string Brand { get; set; }
	[MaxLength(50)]
	public required string Model { get; set; }
	public bool CanUpdate { get; set; }
	public bool CanDelete { get; set; }
}
