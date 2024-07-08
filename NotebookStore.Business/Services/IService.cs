namespace NotebookStore.Business;

public interface IService<T>
{
	/// <summary>
	/// Create a new entity
	/// </summary>
	/// <param name="entity"> The entity to create </param>
	/// <returns> True if the entity was created successfully </returns>
	public Task<bool> Create(T entity);

	/// <summary>
	/// Get all entities
	/// </summary>
	/// <returns> A list of entities </returns>
	public Task<IEnumerable<T>> GetAll();

	/// <summary>
	/// Find an entity by its id
	/// </summary>
	/// <param name="id"> The id of the entity to find </param>
	/// <returns> The entity if found, null otherwise </returns>
	public Task<T?> Find(int id);

	/// <summary>
	/// Update an entity
	/// </summary>
	/// <param name="entity"> The entity to update </param>
	/// <returns> True if the entity was updated successfully </returns>
	public Task<bool> Update(T entity);

	/// <summary>
	/// Delete an entity by its id
	/// </summary>
	/// <param name="id"> The id of the entity to delete </param>
	/// <returns> True if the entity was deleted successfully </returns>
	public Task<bool> Delete(int id);
}
