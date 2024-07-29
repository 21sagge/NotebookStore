﻿namespace NotebookStore.Business;

public class UserDto
{
	public required string Id { get; set; }
	public required string Name { get; set; }
	public required string Email { get; set; }
	public required string Password { get; set; }
	public required string[] Roles { get; set; }
	public string[] Claims { get; set; } = Array.Empty<string>();
}
