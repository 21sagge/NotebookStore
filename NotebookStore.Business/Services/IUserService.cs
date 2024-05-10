namespace NotebookStore.Business
{
    public interface IUserService
    {
        Task<UserDto> GetCurrentUser();
        Task<UserDto> GetUser(string id);
        Task<UserDto> GetUserByEmail(string email);
        Task<UserDto> GetUserByUserName(string userName);
        Task<UserDto?> AddUser(UserDto user);
        Task<UserDto?> UpdateUser(UserDto user);
        Task<UserDto?> DeleteUser(string id);
    }
}