namespace NotebookStoreMVC.Repositories;

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NotebookStore.Entities;
using NotebookStoreContext;
using NotebookStoreMVC.Models;

public class DisplayRepository : IRepository<DisplayViewModel>
{
  private readonly NotebookStoreContext _context;
  private readonly IMapper mapper;

  public DisplayRepository(NotebookStoreContext context, IMapper mapper)
  {
    _context = context;
    this.mapper = mapper;
  }

  public async Task Create(DisplayViewModel entity)
  {
    await _context.Displays.AddAsync(mapper.Map<Display>(entity));
    await _context.SaveChangesAsync();
  }
  public async Task<IEnumerable<DisplayViewModel>> Read()
  {
    return mapper.Map<IEnumerable<DisplayViewModel>>(await _context.Displays.ToListAsync());
  }
  public async Task<DisplayViewModel?> Find(int? id)
  {
    return mapper.Map<DisplayViewModel>(await _context.Displays.FirstOrDefaultAsync(m => m.Id == id));
  }
  public async Task Update(DisplayViewModel entity)
  {
    _context.Displays.Update(mapper.Map<Display>(entity));
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
