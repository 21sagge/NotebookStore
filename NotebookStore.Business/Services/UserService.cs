using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NotebookStore.Business.Context;

namespace NotebookStore.Business;

internal class UserService : IUserService
{
    private readonly IMapper mapper;
    private readonly IUserContext context;
    private readonly UserManager<IdentityUser> userManager;
    private readonly IRoleService roleService;

    public UserService(
        IMapper _mapper,
        IUserContext _context,
        UserManager<IdentityUser> _userManager,
        IRoleService _roleService)
    {
        mapper = _mapper;
        context = _context;
        userManager = _userManager;
        roleService = _roleService;
    }

    public async Task<IEnumerable<UserDto>> GetUsers()
    {
        var users = await userManager.Users.ToListAsync();

        var userDtos = users.Select(async u => await MapUserAsync(u));

        return await Task.WhenAll(userDtos);
    }

    public async Task<UserDto> GetCurrentUser()
    {
        var user = await userManager.GetUserAsync(context.GetCurrentUser() ?? throw new Exception("User not found"))
            ?? throw new Exception("User not found");

        return await MapUserAsync(user);
    }

    public async Task<UserDto> GetUser(string id)
    {
        var user = await userManager.FindByIdAsync(id);

        return await MapUserAsync(user);
    }

    public async Task<bool> DeleteUser(string id)
    {
        var user = await userManager.FindByIdAsync(id);

        if (user == null)
        {
            return false;
        }

        var result = await userManager.DeleteAsync(user);

        return result.Succeeded;
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

        IEnumerable<Task<UserDto>> userDtos = users.Select(async u => await MapUserAsync(u));

        IEnumerable<UserDto> dtos = await Task.WhenAll(userDtos);

        return dtos;
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

    private async Task<UserDto> MapUserAsync(IdentityUser user)
    {
        var userDto = mapper.Map<UserDto>(user);

        var userRoles = await userManager.GetRolesAsync(user);

        userDto.Roles = userRoles.ToArray() ?? Array.Empty<string>();

        var claims = new List<string>();

        foreach (var role in userDto.Roles)
        {
            var roleClaims = await roleService.GetClaims(role);

            claims.AddRange(roleClaims);
        }

        userDto.Claims = claims.ToArray();

        return userDto;
    }
}
