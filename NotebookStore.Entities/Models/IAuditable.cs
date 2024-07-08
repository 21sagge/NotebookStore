namespace NotebookStore.Entities;

public interface IAuditable
{
	string? CreatedBy { get; set; }
	string CreatedAt { get; set; }
}
