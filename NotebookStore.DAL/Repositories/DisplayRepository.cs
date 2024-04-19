namespace NotebookStore.DAL;

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

  public async Task Create(Display entity)
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
    return await _context.Displays.FirstOrDefaultAsync(m => m.Id == id);
  }
  public async Task Update(Display entity)
  {
    _context.Displays.Update(entity);
    await _context.SaveChangesAsync();
  }
  public async Task Delete(int id)
  {
    var display = await _context.Displays.FindAsync(id);
    if (display == null) return;
    _context.Displays.Remove(display);
    await _context.SaveChangesAsync();
  }
  public void Dispose()
  {
    _context.Dispose();
  }
}
