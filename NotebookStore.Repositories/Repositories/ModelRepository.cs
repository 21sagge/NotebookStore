namespace NotebookStore.Repositories;

using Microsoft.EntityFrameworkCore;
using NotebookStore.Entities;
using NotebookStoreContext;

public class ModelRepository : IRepository<Model>
{
  private readonly NotebookStoreContext _context;

  public ModelRepository(NotebookStoreContext context)
  {
    _context = context;
  }

  public async Task Create(Model entity)
  {
    await _context.Models.AddAsync(entity);
    await _context.SaveChangesAsync();
  }
  public async Task<IEnumerable<Model>> Read()
  {
    return await _context.Models.ToListAsync();
  }
  public async Task<Model?> Find(int? id)
  {
    return await _context.Models.FirstOrDefaultAsync(m => m.Id == id);
  }
  public async Task Update(Model entity)
  {
    _context.Models.Update(entity);
    await _context.SaveChangesAsync();
  }

  public async Task Delete(int id)
  {
    var model = await _context.Models.FindAsync(id);
    if (model == null) return;
    _context.Models.Remove(model);
    await _context.SaveChangesAsync();
  }
  public void Dispose()
  {
    _context.Dispose();
  }
}
