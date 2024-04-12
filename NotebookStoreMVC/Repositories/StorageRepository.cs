namespace NotebookStoreMVC.Repositories;

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NotebookStore.Entities;
using NotebookStoreContext;
using NotebookStoreMVC.Models;

public class StorageRepository : IRepository<StorageViewModel>
{
  private readonly NotebookStoreContext _context;
  private readonly IMapper mapper;

  public StorageRepository(NotebookStoreContext context, IMapper mapper)
  {
    _context = context;
    this.mapper = mapper;
  }

  public async Task Create(StorageViewModel entity)
  {
    await _context.Storages.AddAsync(mapper.Map<Storage>(entity));
    await _context.SaveChangesAsync();
  }
  public async Task<IEnumerable<StorageViewModel>> Read()
  {
    return mapper.Map<IEnumerable<StorageViewModel>>(await _context.Storages.ToListAsync());
  }
  public async Task<StorageViewModel?> Find(int? id)
  {
    return mapper.Map<StorageViewModel>(await _context.Storages.FirstOrDefaultAsync(m => m.Id == id));
  }
  public async Task Update(StorageViewModel entity)
  {
    _context.Storages.Update(mapper.Map<Storage>(entity));
    await _context.SaveChangesAsync();
  }

  public async Task Delete(int id)
  {
    var storage = await _context.Storages.FindAsync(id);
    if (storage == null) return;
    _context.Storages.Remove(storage);
    await _context.SaveChangesAsync();
  }
  public void Dispose()
  {
    _context.Dispose();
  }
}
