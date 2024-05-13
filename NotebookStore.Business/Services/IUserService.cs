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
        /// Retrieves a user by their email address.
        /// </summary>
        /// <param name="email">The email address of the user to retrieve.</param>
        /// <returns>The user as a UserDto object.</returns>
        Task<UserDto> GetUserByEmail(string email);

        /// <summary>
        /// Retrieves a user by their username.
        /// </summary>
        /// <param name="userName">The username of the user to retrieve.</param>
        /// <returns>The user as a UserDto object.</returns>
        Task<UserDto> GetUserByUserName(string userName);

        /// <summary>
        /// Adds a user.
        /// </summary>
        /// <param name="user">The user to add.</param>
        /// <returns>The added user as a UserDto object.</returns>
        Task<UserDto?> AddUser(UserDto user);

        /// <summary>
        /// Updates a user.
        /// </summary>
        /// <param name="user">The user to update.</param>
        /// <returns>The updated user as a UserDto object.</returns>
        Task<UserDto?> UpdateUser(UserDto user);

        /// <summary>
        /// Deletes a user.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns>The deleted user as a UserDto object.</returns>
        Task<UserDto?> DeleteUser(string id);

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
        /// Retrieves all users in a role.
        /// </summary>
        /// <param name="roleName">The name of the role whose users to retrieve.</param>
        /// <returns>All users in the role as a collection of UserDto objects.</returns>
        Task<bool> AddUserToRole(string id, string role);

        /// <summary>
        /// Removes a role from a user.
        /// </summary>
        /// <param name="id">The ID of the user to remove the role from.</param>
        /// <param name="role">The role to remove from the user.</param>
        /// <returns>True if the role was removed; otherwise, false.</returns>
        Task<bool> RemoveUserFromRole(string id, string role);

        /// <summary>
        /// Determines whether a user is in a role.
        /// </summary>
        /// <param name="id">The ID of the user to check.</param>
        /// <param name="role">The role to check.</param>
        /// <returns>True if the user is in the role; otherwise, false.</returns>
        Task<bool> IsInRole(string id, string role);
    }
}