namespace NotebookStore.Repositories;

using Microsoft.EntityFrameworkCore;
using NotebookStoreContext;
using AutoMapper;
using NotebookStore.Entities;

public class NotebookRepository : INotebookRepository
{
  private readonly NotebookStoreContext _context;

  public NotebookRepository(NotebookStoreContext context)
  {
    _context = context;
  }

  public async Task Create(Notebook entity)
  {
    await _context.Notebooks.AddAsync(entity);
    await _context.SaveChangesAsync();
  }
  public async Task<IEnumerable<Notebook>> Read()
  {
    var notebooks = await _context.Notebooks
      .Include(n => n.Brand)
      .Include(n => n.Cpu)
      .Include(n => n.Display)
      .Include(n => n.Memory)
      .Include(n => n.Model)
      .Include(n => n.Storage)
      .ToListAsync();

    return notebooks;
  }
  public async Task<Notebook?> Find(int? id)
  {
    var notebooks = await _context.Notebooks
      .Include(n => n.Brand)
      .Include(n => n.Cpu)
      .Include(n => n.Display)
      .Include(n => n.Memory)
      .Include(n => n.Model)
      .Include(n => n.Storage)
      .FirstOrDefaultAsync(n => n.Id == id);

    return notebooks;
  }
  public async Task Update(Notebook entity)
  {
    _context.Notebooks.Update(entity);
    await _context.SaveChangesAsync();
  }

  public async Task Delete(int id)
  {
    var notebook = await _context.Notebooks.FindAsync(id);
    if (notebook == null) return;
    _context.Notebooks.Remove(notebook);
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
