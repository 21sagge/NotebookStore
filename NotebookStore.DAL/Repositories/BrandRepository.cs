namespace NotebookStore.DAL;

using Microsoft.EntityFrameworkCore;
using NotebookStoreContext;
using NotebookStore.Entities;

public class BrandRepository : IRepository<Brand>
{
	private readonly NotebookStoreContext _context;

	public BrandRepository(NotebookStoreContext context)
	{
		_context = context;
	}

	public async Task Create(Brand entity)
	{
		await _context.Brands.AddAsync(entity);
		await _context.SaveChangesAsync();
	}
	public async Task<IEnumerable<Brand>> Read()
	{
		return await _context.Brands.ToListAsync();
	}
	public async Task<Brand?> Find(int? id)
	{
		return await _context.Brands.FirstOrDefaultAsync(m => m.Id == id);
	}
	public async Task Update(Brand entity)
	{
		_context.Brands.Update(entity);
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
