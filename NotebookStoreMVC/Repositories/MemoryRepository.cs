namespace NotebookStoreMVC.Repositories;

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NotebookStore.Entities;
using NotebookStoreContext;
using NotebookStoreMVC.Models;

public class MemoryRepository : IRepository<MemoryViewModel>
{
  private readonly NotebookStoreContext _context;
  private readonly IMapper mapper;

  public MemoryRepository(NotebookStoreContext context, IMapper mapper)
  {
    _context = context;
    this.mapper = mapper;
  }

  public async Task Create(MemoryViewModel entity)
  {
    await _context.Memories.AddAsync(mapper.Map<Memory>(entity));
    await _context.SaveChangesAsync();
  }
  public async Task<IEnumerable<MemoryViewModel>> Read()
  {
    return mapper.Map<IEnumerable<MemoryViewModel>>(await _context.Memories.ToListAsync());
  }
  public async Task<MemoryViewModel?> Find(int? id)
  {
    return mapper.Map<MemoryViewModel>(await _context.Memories.FirstOrDefaultAsync(m => m.Id == id));
  }
  public async Task Update(MemoryViewModel entity)
  {
    _context.Memories.Update(mapper.Map<Memory>(entity));
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
