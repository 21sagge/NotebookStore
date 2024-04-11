namespace NotebookStoreMVC.Repositories;

public interface IRepository<T> : IDisposable
{
	public void Create(T entity);
	public Task<IEnumerable<T>> Read();
	public Task<T?> Find(int? id);
	public void Update(T entity);
	public void Delete(T entity);
}