using System.ComponentModel.DataAnnotations;

namespace NotebookStore.Entities;

public class Memory
{
  public int Id { get; set; }

  /// <summary>
  /// Capacity of the memory in gigabytes (e.g. 8, 16)
  /// </summary>
  [Range(1, 128)]
  public required int Capacity { get; set; }

  /// <summary>
  /// Speed of the memory in MHz (e.g. 2400, 3200)
  /// </summary>
  [Range(800, 6400)]
  public required int Speed { get; set; }

  public string CapacityAndSpeed => $"{Capacity}GB {Speed}MHz";
}