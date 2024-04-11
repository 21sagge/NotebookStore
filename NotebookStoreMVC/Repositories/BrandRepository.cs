namespace NotebookStoreMVC.Repositories;

using Microsoft.EntityFrameworkCore;
using NotebookStore.Entities;
using NotebookStoreContext;

public class BrandRepository : IRepository<Brand>
{
	private readonly NotebookStoreContext _context;

	public BrandRepository(NotebookStoreContext context)
	{
		_context = context;
	}

	public async void Create(Brand entity)
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
		return await _context.Brands.FindAsync(id);
	}
	public async void Update(Brand entity)
	{
		_context.Brands.Update(entity);
		await _context.SaveChangesAsync();
	}

	public async void Delete(Brand entity)
	{
		_context.Brands.Remove(entity);
		await _context.SaveChangesAsync();
	}
	public void Dispose()
	{
		_context.Dispose();
	}
}
