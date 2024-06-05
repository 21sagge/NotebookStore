using System.ComponentModel.DataAnnotations;

namespace NotebookStore.Entities;

public class Storage
{
    public int Id { get; set; }

    /// <summary>
    /// Type of the storage (e.g. SSD, HDD)
    /// </summary>
    [MaxLength(10)]
    public required string Type { get; set; }

    /// <summary>
    /// Capacity of the storage in gigabytes (e.g. 256, 512)
    /// </summary>
    [Range(128, 4096)]
    public required int Capacity { get; set; }
    public string? CreatedBy { get; set; }
    public required string CreatedAt { get; set; }
}