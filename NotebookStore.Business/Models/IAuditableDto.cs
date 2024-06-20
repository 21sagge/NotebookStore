namespace NotebookStore.Business;

public interface IAuditableDto
{
	public bool CanUpdate { get; set; }
	public bool CanDelete { get; set; }
}
