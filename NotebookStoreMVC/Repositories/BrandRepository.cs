namespace NotebookStoreMVC.Repositories;

using Microsoft.EntityFrameworkCore;
using NotebookStoreMVC.Models;
using NotebookStoreContext;
using AutoMapper;
using NotebookStore.Entities;

public class BrandRepository : IRepository<BrandViewModel>
{
	private readonly NotebookStoreContext _context;
	private readonly IMapper mapper;

	public BrandRepository(NotebookStoreContext context, IMapper mapper)
	{
		_context = context;
		this.mapper = mapper;
	}

	public async Task Create(BrandViewModel entity)
	{
		await _context.Brands.AddAsync(mapper.Map<Brand>(entity));
		await _context.SaveChangesAsync();
	}
	public async Task<IEnumerable<BrandViewModel>> Read()
	{
		return mapper.Map<IEnumerable<BrandViewModel>>(await _context.Brands.ToListAsync());
	}
	public async Task<BrandViewModel?> Find(int? id)
	{
		return mapper.Map<BrandViewModel>(await _context.Brands.FirstOrDefaultAsync(m => m.Id == id));
	}
	public async Task Update(BrandViewModel entity)
	{
		_context.Brands.Update(mapper.Map<Brand>(entity));
		await _context.SaveChangesAsync();
	}

	public async Task Delete(int id)
	{
		var brand = await _context.Brands.FindAsync(id);
		if (brand == null) return;
		_context.Brands.Remove(brand);
		await _context.SaveChangesAsync();
	}

	public void Dispose()
	{
		_context.Dispose();
	}
}
