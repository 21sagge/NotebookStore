namespace NotebookStoreMVC.Repositories;

using Microsoft.EntityFrameworkCore;
using NotebookStore.Entities;
using NotebookStoreContext;

public class MemoryRepository : IRepository<Memory>
{
  private readonly NotebookStoreContext _context;

  public MemoryRepository(NotebookStoreContext context)
  {
    _context = context;
  }

  public async void Create(Memory entity)
  {
    await _context.Memories.AddAsync(entity);
    await _context.SaveChangesAsync();
  }
  public async Task<IEnumerable<Memory>> Read()
  {
    return await _context.Memories.ToListAsync();
  }
  public async Task<Memory?> Find(int? id)
  {
    return await _context.Memories.FindAsync(id);
  }
  public async void Update(Memory entity)
  {
    _context.Memories.Update(entity);
    await _context.SaveChangesAsync();
  }

  public async void Delete(Memory entity)
  {
    _context.Memories.Remove(entity);
    await _context.SaveChangesAsync();
  }
  public void Dispose()
  {
    _context.Dispose();
  }
}
