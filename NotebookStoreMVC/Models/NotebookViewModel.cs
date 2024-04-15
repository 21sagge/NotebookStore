using System.ComponentModel.DataAnnotations;
using NotebookStore.Entities;

namespace NotebookStoreMVC.Models;

public class NotebookViewModel
{
    public int Id { get; set; }
    [MaxLength(50)]
    public string? Color { get; set; }
    [Range(0, 10000)]
    public int Price { get; set; }
    public int BrandId { get; set; }
    public virtual BrandViewModel? Brand { get; set; }
    public int ModelId { get; set; }
    public virtual ModelViewModel? Model { get; set; }
    public int CpuId { get; set; }
    public virtual CpuViewModel? Cpu { get; set; }
    public int DisplayId { get; set; }
    public virtual DisplayViewModel? Display { get; set; }
    public int MemoryId { get; set; }
    public virtual MemoryViewModel? Memory { get; set; }
    public int StorageId { get; set; }
    public virtual StorageViewModel? Storage { get; set; }

    public IEnumerable<BrandViewModel>? Brands { get; set; }
    public IEnumerable<ModelViewModel>? Models { get; set; }
    public IEnumerable<CpuViewModel>? Cpus { get; set; }
    public IEnumerable<DisplayViewModel>? Displays { get; set; }
    public IEnumerable<MemoryViewModel>? Memories { get; set; }
    public IEnumerable<StorageViewModel>? Storages { get; set; }
}
