namespace NotebookStoreMVC.Repositories;

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NotebookStore.Entities;
using NotebookStoreContext;
using NotebookStoreMVC.Models;

public class ModelRepository : IRepository<ModelViewModel>
{
  private readonly NotebookStoreContext _context;
  private readonly IMapper mapper;

  public ModelRepository(NotebookStoreContext context, IMapper mapper)
  {
    _context = context;
    this.mapper = mapper;
  }

  public async Task Create(ModelViewModel entity)
  {
    await _context.Models.AddAsync(mapper.Map<Model>(entity));
    await _context.SaveChangesAsync();
  }
  public async Task<IEnumerable<ModelViewModel>> Read()
  {
    return mapper.Map<IEnumerable<ModelViewModel>>(await _context.Models.ToListAsync());
  }
  public async Task<ModelViewModel?> Find(int? id)
  {
    return mapper.Map<ModelViewModel>(await _context.Models.FirstOrDefaultAsync(m => m.Id == id));
  }
  public async Task Update(ModelViewModel entity)
  {
    _context.Models.Update(mapper.Map<Model>(entity));
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
