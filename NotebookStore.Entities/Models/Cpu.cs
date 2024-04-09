using System.ComponentModel.DataAnnotations;

namespace NotebookStore.Entities;

public class Cpu
{
  public int Id { get; set; }
  [MaxLength(50)]
  public required string Brand { get; set; }
  [MaxLength(50)]
  public required string Model { get; set; }

  public string Name => $"{Brand} {Model}";
}