namespace NotebookStore.Business
{
    public interface IUserService
    {
        /// <summary>
        /// Retrieves all users.
        /// </summary>
        /// <returns>All users as a collection of UserDto objects.</returns>
        Task<IEnumerable<UserDto>> GetUsers();

        /// <summary>
        /// Retrieves the current user.
        /// </summary>
        /// <returns>The current user as a UserDto object.</returns>
        Task<UserDto> GetCurrentUser();

        /// <summary>
        /// Retrieves a user by their ID.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <returns>The user as a UserDto object.</returns>
        Task<UserDto> GetUser(string id);

        /// <summary>
        /// Deletes a user.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns>The deleted user as a UserDto object.</returns>
        Task<bool> DeleteUser(string id);

        /// <summary>
        /// Retrieves the roles of a user.
        /// </summary>
        /// <param name="id">The ID of the user whose roles to retrieve.</param>
        /// <returns>The roles of the user as a collection of strings.</returns>
        Task<IEnumerable<string>?> GetUserRoles(string id);

        /// <summary>
        /// Adds a role to a user.
        /// </summary>
        /// <param name="id">The ID of the user to add the role to.</param>
        /// <param name="role">The role to add to the user.</param>
        /// <returns>True if the role was added; otherwise, false.</returns>
        Task<IEnumerable<UserDto>> GetUsersInRole(string roleName);

        /// <summary>
        /// Adds a role to a user.
        /// </summary>
        /// <param name="id">The ID of the user to add the role to.</param>
        /// <param name="roles">The roles to add to the user.</param>
        /// <returns>True if the roles were added; otherwise, false.</returns>
        Task<bool> AddUserRoles(string id, string[] roles);

        /// <summary>
        /// Retrieves all roles.
        /// </summary>
        /// <param name="id">The ID of the user whose roles to retrieve.</param>
        /// <param name="roles">The roles to add to the user.</param>
        /// <returns>True if the roles were added; otherwise, false.</returns>
        Task<bool> RemoveUserRoles(string id, string[] roles);
    }
}