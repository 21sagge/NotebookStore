namespace NotebookStore.Business;

public class RoleDto
{
	public required string Id { get; set; }
	public required string Name { get; set; }
	public List<string> Claims { get; set; } = new List<string>();
}
