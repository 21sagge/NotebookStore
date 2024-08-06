using System.ComponentModel.DataAnnotations;

namespace NotebookStore.Entities;

public class Cpu : IAuditable
{
  public int Id { get; set; }
  [MaxLength(50)]
  public string Brand { get; set; } = string.Empty;
  [MaxLength(50)]
  public string Model { get; set; } = string.Empty;
  public string? CreatedBy { get; set; }
  public string CreatedAt { get; set; } = DateTime.Now.ToString();
}