namespace NotebookStore.Business;

public class RoleDto
{
	public string? Id { get; set; }
	public required string Name { get; set; }
	public List<string> Claims { get; set; } = new List<string>();
}
