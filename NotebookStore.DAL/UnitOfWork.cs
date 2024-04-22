using NotebookStore.Entities;

namespace NotebookStore.DAL;

public class UnitOfWork : IUnitOfWork
{
    private readonly NotebookStoreContext.NotebookStoreContext context;

    public UnitOfWork(NotebookStoreContext.NotebookStoreContext context)
    {
        this.context = context;
    }

    public IRepository<Brand> Brands => new BrandRepository(context);

    public IRepository<Cpu> Cpus => new CpuRepository(context);

    public IRepository<Display> Displays => new DisplayRepository(context);

    public IRepository<Memory> Memories => new MemoryRepository(context);

    public IRepository<Model> Models => new ModelRepository(context);

    public IRepository<Storage> Storages => new StorageRepository(context);

    public IRepository<Notebook> Notebooks => new NotebookRepository(context);

    public async Task SaveAsync() => await context.SaveChangesAsync();

    public void BeginTransaction() => context.Database.BeginTransaction();

    public void CommitTransaction() => context.Database.CommitTransaction();

    public void RollbackTransaction() => context.Database.RollbackTransaction();
}
