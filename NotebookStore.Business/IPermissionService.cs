using NotebookStore.Entities;

namespace NotebookStore.Business;

public interface IPermissionService
{
	/// <summary>
	/// Assigns permission to the entity based on the current user.
	/// </summary>
	/// <param name="entity"> The entity to assign permission to. </param>
	/// <param name="currentUser"> The current user. </param>
	/// <typeparam name="T"> The type of the entity. </typeparam>
	/// <typeparam name="TDto"> The type of the DTO. </typeparam>
	/// <returns> The DTO with the permission assigned. </returns>
	public TDto AssignPermission<T, TDto>(T entity, UserDto currentUser)
		where T : IAuditable
		where TDto : IAuditableDto;
}
