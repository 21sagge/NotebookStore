using NotebookStore.Entities;

namespace NotebookStore.DAL;

public interface IUnitOfWork
{
    IRepository<Brand> Brands { get; }
    IRepository<Cpu> Cpus { get; }
    IRepository<Display> Displays { get; }
    IRepository<Memory> Memories { get; }
    IRepository<Model> Models { get; }
    IRepository<Storage> Storages { get; }
    IRepository<Notebook> Notebooks { get; }
    Task SaveAsync();
    void BeginTransaction();
    void CommitTransaction();
    void RollbackTransaction();
}
