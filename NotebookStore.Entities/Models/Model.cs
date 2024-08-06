using System.ComponentModel.DataAnnotations;

namespace NotebookStore.Entities;

public class Model : IAuditable
{
  public int Id { get; set; }
  [MaxLength(50)]
  public string Name { get; set; } = string.Empty;
  public string? CreatedBy { get; set; }
  public string CreatedAt { get; set; } = string.Empty;
}