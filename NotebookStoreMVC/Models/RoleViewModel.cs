namespace NotebookStoreMVC.Models;

public class RoleViewModel
{	
	public string? Id { get; set; }
	public required string Name { get; set; }
	public string? Description { get; set; }
	public List<string>? Claims { get; set; }
	public List<string>? AllClaims { get; set; }

}
