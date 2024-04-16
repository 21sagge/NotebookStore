namespace NotebookStore.Repositories;

using NotebookStore.Entities;

public interface INotebookRepository : IRepository<Notebook>
{
  IEnumerable<Brand> Brands { get; }
  IEnumerable<Cpu> Cpus { get; }
  IEnumerable<Display> Displays { get; }
  IEnumerable<Memory> Memories { get; }
  IEnumerable<Model> Models { get; }
  IEnumerable<Storage> Storages { get; }
}
