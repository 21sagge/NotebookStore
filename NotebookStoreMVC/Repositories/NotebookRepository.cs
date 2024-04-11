namespace NotebookStoreMVC.Repositories;

using Microsoft.EntityFrameworkCore;
using NotebookStore.Entities;
using NotebookStoreContext;

public class NotebookRepository : INotebookRepository
{
  private readonly NotebookStoreContext _context;

  public NotebookRepository(NotebookStoreContext context)
  {
    _context = context;
  }

  public async void Create(Notebook entity)
  {
    await _context.Notebooks.AddAsync(entity);
    await _context.SaveChangesAsync();
  }
  public async Task<IEnumerable<Notebook>> Read()
  {
    return await _context.Notebooks.Include(n => n.Brand).Include(n => n.Cpu).Include(n => n.Display).Include(n => n.Memory).Include(n => n.Model).Include(n => n.Storage).ToListAsync();
  }
  public async Task<Notebook?> Find(int? id)
  {
    return await _context.Notebooks.Include(n => n.Brand).Include(n => n.Cpu).Include(n => n.Display).Include(n => n.Memory).Include(n => n.Model).Include(n => n.Storage).FirstOrDefaultAsync(m => m.Id == id);
  }
  public async void Update(Notebook entity)
  {
    _context.Notebooks.Update(entity);
    await _context.SaveChangesAsync();
  }

  public async void Delete(Notebook entity)
  {
    _context.Notebooks.Remove(entity);
    await _context.SaveChangesAsync();
  }
  public void Dispose()
  {
    _context.Dispose();
  }

  public IEnumerable<Brand> Brands => _context.Brands;
  public IEnumerable<Cpu> Cpus => _context.Cpus;
  public IEnumerable<Display> Displays => _context.Displays;
  public IEnumerable<Memory> Memories => _context.Memories;
  public IEnumerable<Model> Models => _context.Models;
  public IEnumerable<Storage> Storages => _context.Storages;
}
