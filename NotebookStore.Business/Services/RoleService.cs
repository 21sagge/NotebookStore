using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace NotebookStore.Business;

internal class RoleService : IRoleService
{
	private readonly RoleManager<IdentityRole> roleManager;
	private readonly IMapper mapper;

	public RoleService(RoleManager<IdentityRole> _roleManager, IMapper _mapper)
	{
		roleManager = _roleManager;
		mapper = _mapper;
	}

	public async Task<bool> CreateRole(string role)
	{
		var result = await roleManager.CreateAsync(new IdentityRole(role));

		return result.Succeeded;
	}

	public async Task<bool> DeleteRole(string role)
	{
		var IdentityRole = await roleManager.FindByNameAsync(role);

		if (IdentityRole == null) return false;

		var result = await roleManager.DeleteAsync(IdentityRole);

		return result.Succeeded;
	}

	public async Task<IEnumerable<RoleDto?>> GetRoles()
	{
		var roles = await roleManager.Roles.ToListAsync();

		return mapper.Map<IEnumerable<RoleDto>>(roles);
	}

	public async Task<RoleDto?> GetRole(string role)
	{
		var IdentityRole = await roleManager.FindByNameAsync(role);

		return mapper.Map<RoleDto>(IdentityRole);
	}

	public async Task<IEnumerable<string>> GetClaims(string role)
	{
		var IdentityRole = await roleManager.FindByNameAsync(role);

		if (IdentityRole == null) return new List<string>();

		var claims = await roleManager.GetClaimsAsync(IdentityRole);

		return claims.Select(c => c.Value);
	}

	public async Task<bool> UpdateRole(string role, List<string> claims)
	{
		if (claims == null) throw new ArgumentNullException(nameof(claims), "Cannot be null");
		if (string.IsNullOrEmpty(role)) throw new ArgumentNullException(nameof(role), "Cannot be empty or null");

		var IdentityRole = await roleManager.FindByNameAsync(role);

		if (IdentityRole == null) return false;

		var existingClaims = await roleManager.GetClaimsAsync(IdentityRole);

		// Remove claims
		foreach (var claim in existingClaims)
		{
			if (claims.Any(c => c == claim.Value)) continue;

			await roleManager.RemoveClaimAsync(IdentityRole, claim);
		}

		// Add new claims
		foreach (var claim in claims)
		{
			if (existingClaims.Any(c => c.Value == claim)) continue;

			await roleManager.AddClaimAsync(IdentityRole, new Claim("Permission", claim));
		}

		return true;
	}
}
