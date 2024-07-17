namespace NotebookStore.Business;

using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NotebookStore.Business.Context;

internal class UserService : IUserService
{
    private readonly IMapper mapper;
    private readonly IUserContext context;
    private readonly UserManager<IdentityUser> userManager;

    public UserService(IMapper mapper, IUserContext _context, UserManager<IdentityUser> _userManager)
    {
        this.mapper = mapper;
        context = _context;
        userManager = _userManager;
    }

    public async Task<IEnumerable<UserDto>> GetUsers()
    {
        var users = await userManager.Users.ToListAsync();

        return mapper.Map<IEnumerable<UserDto>>(users);
    }

    public async Task<UserDto> GetCurrentUser()
    {
        var user = await userManager.GetUserAsync(context.GetCurrentUser() ?? throw new Exception("User not found"))
            ?? throw new Exception("User not found");

        var userDto = mapper.Map<UserDto>(user);

        var userRoles = await userManager.GetRolesAsync(user);

        userDto.Roles = userRoles.ToArray() ?? Array.Empty<string>();

        return userDto;
    }

    public async Task<UserDto> GetUser(string id)
    {
        var user = await userManager.FindByIdAsync(id);

        return mapper.Map<UserDto>(user);
    }

    public async Task<UserDto> GetUserByEmail(string email)
    {
        var user = await userManager.FindByEmailAsync(email);

        return mapper.Map<UserDto>(user);
    }

    public async Task<UserDto> GetUserByUserName(string userName)
    {
        var user = await userManager.FindByNameAsync(userName);

        return mapper.Map<UserDto>(user);
    }

    public async Task<UserDto?> AddUser(UserDto user)
    {
        var identityUser = mapper.Map<IdentityUser>(user);

        var result = await userManager.CreateAsync(identityUser, user.Password);

        if (result.Succeeded)
        {
            return mapper.Map<UserDto>(identityUser);
        }

        return null;
    }

    public async Task<UserDto?> UpdateUser(UserDto user)
    {
        var identityUser = mapper.Map<IdentityUser>(user);

        var result = await userManager.UpdateAsync(identityUser);

        if (result.Succeeded)
        {
            return mapper.Map<UserDto>(identityUser);
        }

        return null;
    }

    public async Task<UserDto?> DeleteUser(string id)
    {
        var user = await userManager.FindByIdAsync(id);

        if (user == null)
        {
            return null;
        }

        var result = await userManager.DeleteAsync(user);

        if (result.Succeeded)
        {
            return mapper.Map<UserDto>(user);
        }

        return null;
    }

    public async Task<IEnumerable<string>?> GetUserRoles(string id)
    {
        var user = await userManager.FindByIdAsync(id);

        if (user == null)
        {
            return null;
        }

        return await userManager.GetRolesAsync(user);
    }

    public async Task<IEnumerable<UserDto>> GetUsersInRole(string role)
    {
        var users = await userManager.GetUsersInRoleAsync(role);

        return mapper.Map<IEnumerable<UserDto>>(users);
    }

    public async Task<bool> AddUserToRole(string id, string role)
    {
        var user = await userManager.FindByIdAsync(id);

        if (user == null)
        {
            return false;
        }

        // Limit the number of Admin users to 3
        if (role == "Admin")
        {
            var adminUsers = await GetUsersInRole(role);

            if (adminUsers.Count() >= 3)
            {
                return false;
            }
        }

        var result = await userManager.AddToRoleAsync(user, role);

        return result.Succeeded;
    }

    public async Task<bool> AddUserRoles(string id, string[] roles)
    {
        var user = await userManager.FindByIdAsync(id);

        if (user == null)
        {
            return false;
        }

        var result = await userManager.AddToRolesAsync(user, roles);

        return result.Succeeded;
    }

    public async Task<bool> RemoveUserFromRole(string id, string role)
    {
        var user = await userManager.FindByIdAsync(id);

        if (user == null)
        {
            return false;
        }

        var result = await userManager.RemoveFromRoleAsync(user, role);

        return result.Succeeded;
    }

    public async Task<bool> RemoveUserRoles(string id, string[] roles)
    {
        var user = await userManager.FindByIdAsync(id);

        if (user == null)
        {
            return false;
        }

        var result = await userManager.RemoveFromRolesAsync(user, roles);

        return result.Succeeded;
    }

    public async Task<bool> IsInRole(string id, string role)
    {
        var user = await userManager.FindByIdAsync(id);

        if (user == null)
        {
            return false;
        }

        return await userManager.IsInRoleAsync(user, role);
    }
}
