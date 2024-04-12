namespace NotebookStoreMVC.Repositories;

using Microsoft.EntityFrameworkCore;
using NotebookStoreMVC.Models;
using NotebookStoreContext;
using AutoMapper;
using NotebookStore.Entities;

public class NotebookRepository : INotebookRepository
{
  private readonly NotebookStoreContext _context;
  private readonly IMapper mapper;

  public NotebookRepository(NotebookStoreContext context, IMapper mapper)
  {
    _context = context;
    this.mapper = mapper;
  }

  public async Task Create(NotebookViewModel entity)
  {
    await _context.Notebooks.AddAsync(mapper.Map<Notebook>(entity));
    await _context.SaveChangesAsync();
  }
  public async Task<IEnumerable<NotebookViewModel>> Read()
  {
    var notebooks = await _context.Notebooks
      .Include(n => n.Brand)
      .Include(n => n.Cpu)
      .Include(n => n.Display)
      .Include(n => n.Memory)
      .Include(n => n.Model)
      .Include(n => n.Storage)
      .ToListAsync();

    return mapper.Map<IEnumerable<NotebookViewModel>>(notebooks);
  }
  public async Task<NotebookViewModel?> Find(int? id)
  {
    var notebooks = await _context.Notebooks
      .Include(n => n.Brand)
      .Include(n => n.Cpu)
      .Include(n => n.Display)
      .Include(n => n.Memory)
      .Include(n => n.Model)
      .Include(n => n.Storage)
      .FirstOrDefaultAsync(n => n.Id == id);

    return mapper.Map<NotebookViewModel>(notebooks);
  }
  public async Task Update(NotebookViewModel entity)
  {
    _context.Notebooks.Update(mapper.Map<Notebook>(entity));
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

  public IEnumerable<BrandViewModel> Brands => mapper.Map<IEnumerable<BrandViewModel>>(_context.Brands);
  public IEnumerable<CpuViewModel> Cpus => mapper.Map<IEnumerable<CpuViewModel>>(_context.Cpus);
  public IEnumerable<DisplayViewModel> Displays => mapper.Map<IEnumerable<DisplayViewModel>>(_context.Displays);
  public IEnumerable<MemoryViewModel> Memories => mapper.Map<IEnumerable<MemoryViewModel>>(_context.Memories);
  public IEnumerable<ModelViewModel> Models => mapper.Map<IEnumerable<ModelViewModel>>(_context.Models);
  public IEnumerable<StorageViewModel> Storages => mapper.Map<IEnumerable<StorageViewModel>>(_context.Storages);
}
