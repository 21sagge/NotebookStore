namespace NotebookStoreMVC.Repositories;

using NotebookStoreMVC.Models;

public interface INotebookRepository : IRepository<NotebookViewModel>
{
  IEnumerable<BrandViewModel> Brands { get; }
  IEnumerable<CpuViewModel> Cpus { get; }
  IEnumerable<DisplayViewModel> Displays { get; }
  IEnumerable<MemoryViewModel> Memories { get; }
  IEnumerable<ModelViewModel> Models { get; }
  IEnumerable<StorageViewModel> Storages { get; }
}
