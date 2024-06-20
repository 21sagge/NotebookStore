using System.ComponentModel.DataAnnotations;

namespace NotebookStore.Entities;

public class Brand : IAuditable
{
  public int Id { get; set; }
  [MaxLength(50)]
  public required string Name { get; set; }
  public string? CreatedBy { get; set; }
  public required string CreatedAt { get; set; }
}