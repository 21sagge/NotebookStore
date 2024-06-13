namespace NotebookStore.DAL;

public interface IRepository<T> : IDisposable
{
	/// <summary>
	/// Create a new entity.
	/// </summary>
	public Task Create(T entity);

	/// <summary>
	/// Read all entities.
	/// </summary>
	public Task<IEnumerable<T>> Read();

	/// <summary>
	/// Find an entity by its id.
	/// </summary>
	public Task<T?> Find(int? id);

	/// <summary>
	/// Update an entity.
	/// </summary>
	public Task Update(T entity);

	/// <summary>
	/// Delete an entity by its id.
	/// </summary>
	public Task Delete(int id);
}