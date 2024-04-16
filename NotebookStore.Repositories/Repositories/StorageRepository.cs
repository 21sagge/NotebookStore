namespace NotebookStore.Repositories;

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

  public async Task Create(Storage entity)
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
    return await _context.Storages.FirstOrDefaultAsync(m => m.Id == id);
  }
  public async Task Update(Storage entity)
  {
    _context.Storages.Update(entity);
    await _context.SaveChangesAsync();
  }

  public async Task Delete(int id)
  {
    var storage = await _context.Storages.FindAsync(id);
    if (storage == null) return;
    _context.Storages.Remove(storage);
    await _context.SaveChangesAsync();
  }
  public void Dispose()
  {
    _context.Dispose();
  }
}
