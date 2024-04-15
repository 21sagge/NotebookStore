using System.ComponentModel.DataAnnotations;

namespace NotebookStoreMVC.Models;

public class BrandViewModel
{
  public int Id { get; set; }
  [MaxLength(50)]
  public string? Name { get; set; }
}
