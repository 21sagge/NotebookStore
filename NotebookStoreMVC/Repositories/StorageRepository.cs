namespace NotebookStoreMVC.Repositories;

using Microsoft.EntityFrameworkCore;
using NotebookStore.Entities;
using NotebookStoreContext;

public class StorageRepository : IRepository<Storage>
{
  private readonly NotebookStoreContext _context;

  public StorageRepository(NotebookStoreContext context)
  {
    _context = context;
  }

  public async void Create(Storage entity)
  {
    await _context.Storages.AddAsync(entity);
    await _context.SaveChangesAsync();
  }
  public async Task<IEnumerable<Storage>> Read()
  {
    return await _context.Storages.ToListAsync();
  }
  public async Task<Storage?> Find(int? id)
  {
    return await _context.Storages.FindAsync(id);
  }
  public async void Update(Storage entity)
  {
    _context.Storages.Update(entity);
    await _context.SaveChangesAsync();
  }

  public async void Delete(Storage entity)
  {
    _context.Storages.Remove(entity);
    await _context.SaveChangesAsync();
  }
  public void Dispose()
  {
    _context.Dispose();
  }
}
