namespace NotebookStoreMVC.Repositories;

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

  public async void Create(Cpu entity)
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
    return await _context.Cpus.FindAsync(id);
  }
  public async void Update(Cpu entity)
  {
    _context.Cpus.Update(entity);
    await _context.SaveChangesAsync();
  }

  public async void Delete(Cpu entity)
  {
    _context.Cpus.Remove(entity);
    await _context.SaveChangesAsync();
  }
  public void Dispose()
  {
    _context.Dispose();
  }
}
