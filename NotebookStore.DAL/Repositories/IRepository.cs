namespace NotebookStore.DAL;

public interface IRepository<T> : IDisposable
{
	public Task Create(T entity);
	public Task<IEnumerable<T>> Read();
	public Task<T?> Find(int? id);
	public Task Update(T entity);
	public Task Delete(int id);
}