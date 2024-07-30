namespace NotebookStore.Business;

public interface IRoleService
{
	/// <summary>
	/// Creates a role.
	/// </summary>
	/// <param name="role">The name of the role to create.</param>
	/// <returns>True if the role was created; otherwise, false.</returns>
	Task<bool> CreateRole(string role);

	/// <summary>
	/// Deletes a role.
	/// </summary>
	/// <param name="id">The ID of the role to delete.</param>
	/// <returns>True if the role was deleted; otherwise, false.</returns>
	Task<bool> DeleteRole(string id);

	/// <summary>
	/// Gets all roles.
	/// </summary>
	/// <returns>A collection of roles.</returns>
	Task<IEnumerable<RoleDto?>> GetRoles();

	/// <summary>
	///	Gets a role.
	/// </summary>
	/// <param name="id">The ID of the role to get.</param>
	/// <returns>The role.</returns>
	Task<RoleDto?> GetRole(string id);

	/// <summary>
	///	Gets the claims for a role.
	/// </summary>
	/// <param name="id">The ID of the role to get claims for.</param>
	/// <returns>A collection of claims.</returns>
	Task<IEnumerable<string>> GetClaims(string id);

	/// <summary>
	///	Updates a role with the specified claims.
	/// </summary>
	/// <param name="role">The role to update.</param>
	/// <returns>True if the role was updated; otherwise, false.</returns>
	Task<bool> UpdateRole(RoleDto role);
}
