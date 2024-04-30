using System.ComponentModel.DataAnnotations;

namespace NotebookStore.Entities;

public class User
{
	public int Id { get; set; }
	public required string Name { get; set; }

	[EmailAddress]
	public required string Email { get; set; }

	[DataType(DataType.Password)]
	[MinLength(6)]
	public required string Password { get; set; }
}