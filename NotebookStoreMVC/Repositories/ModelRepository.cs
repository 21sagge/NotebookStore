namespace NotebookStoreMVC.Repositories;

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

  public async void Create(Model entity)
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
    return await _context.Models.FindAsync(id);
  }
  public async void Update(Model entity)
  {
    _context.Models.Update(entity);
    await _context.SaveChangesAsync();
  }

  public async void Delete(Model entity)
  {
    _context.Models.Remove(entity);
    await _context.SaveChangesAsync();
  }
  public void Dispose()
  {
    _context.Dispose();
  }
}
