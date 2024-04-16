namespace NotebookStore.Repositories;

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

  public async Task Create(Memory entity)
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
    return await _context.Memories.FirstOrDefaultAsync(m => m.Id == id);
  }
  public async Task Update(Memory entity)
  {
    _context.Memories.Update(entity);
    await _context.SaveChangesAsync();
  }

  public async Task Delete(int id)
  {
    var memory = await _context.Memories.FindAsync(id);
    if (memory == null) return;
    _context.Memories.Remove(memory);
    await _context.SaveChangesAsync();
  }
  public void Dispose()
  {
    _context.Dispose();
  }
}
