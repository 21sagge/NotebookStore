using System.ComponentModel.DataAnnotations;

namespace NotebookStore.Entities;

public class Brand
{
  public int Id { get; set; }
  [MaxLength(50)]
  public required string Name { get; set; }
}