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
	/// <param name="role">The name of the role to delete.</param>
	/// <returns>True if the role was deleted; otherwise, false.</returns>
	Task<bool> DeleteRole(string role);

	/// <summary>
	/// Gets all roles.
	/// </summary>
	/// <returns>A collection of roles.</returns>
	Task<IEnumerable<string?>> GetRoles();

	/// <summary>
	///	Gets a role.
	/// </summary>
	/// <param name="role">The name of the role to get.</param>
	/// <returns>The role.</returns>
	Task<string?> GetRole(string role);

	/// <summary>
	///	Gets the claims for a role.
	/// </summary>
	/// <param name="role">The name of the role to get claims for.</param>
	/// <returns>A collection of claims.</returns>
	Task<IEnumerable<string>> GetClaims(string role);

	/// <summary>
	///	Gets all claims.
	/// </summary>
	/// <returns>A collection of claims.</returns>
	Task<IEnumerable<string>> GetAllClaims();

	/// <summary>
	///	Updates a role with the specified claims and refreshes the user's claims.
	/// </summary>
	/// <param name="role">The name of the role to update.</param>
	/// <param name="claims">The claims to update.</param>
	/// <returns>True if the role was updated; otherwise, false.</returns>
	Task<bool> UpdateRole(string role, List<string> claims);
}
