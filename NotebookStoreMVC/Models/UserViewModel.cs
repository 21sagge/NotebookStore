using System.ComponentModel.DataAnnotations;

namespace NotebookStoreMVC.Models;

public class UserViewModel
{
	public required string Id { get; set; }
	public string? Name { get; set; }

	[DataType(DataType.EmailAddress)]
	public string? Email { get; set; }
	public string? Password { get; set; }
	public required string[] Roles { get; set; }
}
