using System.ComponentModel.DataAnnotations;

namespace NotebookStore.Entities;

public class UserViewModel
{
	public int Id { get; set; }	
	public required string Name { get; set; }
	
	[DataType(DataType.EmailAddress)]
	[EmailAddress]
	public required string Email { get; set; }
	public required string Password { get; set; }

}