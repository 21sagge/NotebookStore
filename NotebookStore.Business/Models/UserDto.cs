﻿namespace NotebookStore.Business;

public class UserDto
{
	public int Id { get; set; }
	public required string Name { get; set; }
	public required string Email { get; set; }
	public required string Password { get; set; }
	public string? Role { get; set; }
	public string? Token { get; set; }
}
