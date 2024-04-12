namespace NotebookStoreMVC.Repositories;

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NotebookStore.Entities;
using NotebookStoreContext;
using NotebookStoreMVC.Models;

public class CpuRepository : IRepository<CpuViewModel>
{
  private readonly NotebookStoreContext _context;
  private readonly IMapper mapper;

  public CpuRepository(NotebookStoreContext context, IMapper mapper)
  {
    _context = context;
    this.mapper = mapper;
  }

  public async Task Create(CpuViewModel entity)
  {
    await _context.Cpus.AddAsync(mapper.Map<Cpu>(entity));
    await _context.SaveChangesAsync();
  }
  public async Task<IEnumerable<CpuViewModel>> Read()
  {
    return mapper.Map<IEnumerable<CpuViewModel>>(await _context.Cpus.ToListAsync());
  }
  public async Task<CpuViewModel?> Find(int? id)
  {
    return mapper.Map<CpuViewModel>(await _context.Cpus.FirstOrDefaultAsync(m => m.Id == id));
  }
  public async Task Update(CpuViewModel entity)
  {
    _context.Cpus.Update(mapper.Map<Cpu>(entity));
    await _context.SaveChangesAsync();
  }

  public async Task Delete(int id)
  {
    var cpu = await _context.Cpus.FindAsync(id);
    if (cpu == null) return;
    _context.Cpus.Remove(cpu);
    await _context.SaveChangesAsync();
  }
  public void Dispose()
  {
    _context.Dispose();
  }
}
