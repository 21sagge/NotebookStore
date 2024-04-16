namespace NotebookStore.Repositories;

using Microsoft.EntityFrameworkCore;
using NotebookStore.Entities;
using NotebookStoreContext;

public class CpuRepository : IRepository<Cpu>
{
  private readonly NotebookStoreContext _context;

  public CpuRepository(NotebookStoreContext context)
  {
    _context = context;
  }

  public async Task Create(Cpu entity)
  {
    await _context.Cpus.AddAsync(entity);
    await _context.SaveChangesAsync();
  }
  public async Task<IEnumerable<Cpu>> Read()
  {
    return await _context.Cpus.ToListAsync();
  }
  public async Task<Cpu?> Find(int? id)
  {
    return await _context.Cpus.FirstOrDefaultAsync(m => m.Id == id);
  }
  public async Task Update(Cpu entity)
  {
    _context.Cpus.Update(entity);
    await _context.SaveChangesAsync();
  }

  public async Task Delete(int id)
  {
    var cpu = await _context.Cpus.FindAsync(id);
    if (cpu == null) return;
    _context.Cpus.Remove(cpu);
    await _context.SaveChangesAsync();
  }
  public void Dispose()
  {
    _context.Dispose();
  }
}
