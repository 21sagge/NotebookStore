namespace NotebookStore.Business;

using AutoMapper;
using NotebookStore.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

public class UserService : IUserService
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private readonly IHttpContextAccessor context;
    private readonly UserManager<IdentityUser> userManager;

    public UserService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor _context, UserManager<IdentityUser> _userManager)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        context = _context;
        userManager = _userManager;
    }

    public async Task<UserDto> GetCurrentUser()
    {
        var user = await userManager.GetUserAsync(context.HttpContext.User);

        return mapper.Map<UserDto>(user);
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
}
