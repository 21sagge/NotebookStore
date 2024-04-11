namespace NotebookStoreMVC.Repositories;

using Microsoft.EntityFrameworkCore;
using NotebookStore.Entities;
using NotebookStoreContext;

public class DisplayRepository : IRepository<Display>
{
  private readonly NotebookStoreContext _context;

  public DisplayRepository(NotebookStoreContext context)
  {
    _context = context;
  }

  public async void Create(Display entity)
  {
    await _context.Displays.AddAsync(entity);
    await _context.SaveChangesAsync();
  }
  public async Task<IEnumerable<Display>> Read()
  {
    return await _context.Displays.ToListAsync();
  }
  public async Task<Display?> Find(int? id)
  {
    return await _context.Displays.FindAsync(id);
  }
  public async void Update(Display entity)
  {
    _context.Displays.Update(entity);
    await _context.SaveChangesAsync();
  }
  public async void Delete(Display entity)
  {
    _context.Displays.Remove(entity);
    await _context.SaveChangesAsync();
  }
  public void Dispose()
  {
    _context.Dispose();
  }
}
