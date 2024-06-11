using System.ComponentModel.DataAnnotations;

namespace NotebookStoreMVC.Models;

public class ModelViewModel
{
  public int Id { get; set; }
  [MaxLength(50)]
  public required string Name { get; set; }
  public string? CreatedBy { get; set; }
  public required string CreatedAt { get; set; }
}
