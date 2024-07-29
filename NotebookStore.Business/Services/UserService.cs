﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NotebookStore.Business.Context;

namespace NotebookStore.Business;

internal class UserService : IUserService
{
    private readonly IMapper mapper;
    private readonly IUserContext context;
    private readonly UserManager<IdentityUser> userManager;

    public UserService(IMapper _mapper, IUserContext _context, UserManager<IdentityUser> _userManager)
    {
        mapper = _mapper;
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
}
