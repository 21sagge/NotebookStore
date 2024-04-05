namespace NotebookStore.Entities;

public class Storage
{
  public int Id { get; set; }

  /// <summary>
  /// Type of the storage (e.g. SSD, HDD)
  /// </summary>
  public required string Type { get; set; }

  /// <summary>
  /// Capacity of the storage in gigabytes (e.g. 256, 512)
  /// </summary>
  public required int Capacity { get; set; }
}