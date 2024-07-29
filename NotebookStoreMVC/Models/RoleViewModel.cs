namespace NotebookStoreMVC.Models;

public class RoleViewModel
{
	public required string Id { get; set; }
	public required string Name { get; set; }
	public List<string> Claims { get; set; } = new List<string>();
}
