namespace NotebookStore.Business;

public interface IService<T>
{
	public Task<bool> Create(T entity);
	public Task<IEnumerable<T>> GetAll();
	public Task<T?> Find(int id);
	public Task<bool> Update(T entity);
	public Task<bool> Delete(int id);
}
