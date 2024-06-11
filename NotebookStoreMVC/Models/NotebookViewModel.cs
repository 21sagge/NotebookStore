using System.ComponentModel.DataAnnotations;

namespace NotebookStoreMVC.Models;

public class NotebookViewModel
{
    public int Id { get; set; }
    [MaxLength(50)]
    public string? Color { get; set; }
    [Range(0, 10000)]
    public int Price { get; set; }
    public int BrandId { get; set; }
    public BrandViewModel? Brand { get; set; }
    public int ModelId { get; set; }
    public ModelViewModel? Model { get; set; }
    public int CpuId { get; set; }
    public CpuViewModel? Cpu { get; set; }
    public int DisplayId { get; set; }
    public DisplayViewModel? Display { get; set; }
    public int MemoryId { get; set; }
    public MemoryViewModel? Memory { get; set; }
    public int StorageId { get; set; }
    public StorageViewModel? Storage { get; set; }
    public string? CreatedBy { get; set; }
    public string? CreatedAt { get; set; }

    public IEnumerable<BrandViewModel>? Brands { get; set; }
    public IEnumerable<ModelViewModel>? Models { get; set; }
    public IEnumerable<CpuViewModel>? Cpus { get; set; }
    public IEnumerable<DisplayViewModel>? Displays { get; set; }
    public IEnumerable<MemoryViewModel>? Memories { get; set; }
    public IEnumerable<StorageViewModel>? Storages { get; set; }
}
