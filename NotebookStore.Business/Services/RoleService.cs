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

	public async Task<bool> CreateRole(string name)
	{
		var role = new IdentityRole(name);

		var result = await roleManager.CreateAsync(role);

		return result.Succeeded;
	}

	public async Task<bool> DeleteRole(string id)
	{
		var IdentityRole = await roleManager.FindByIdAsync(id);

		if (IdentityRole == null) return false;

		var result = await roleManager.DeleteAsync(IdentityRole);

		return result.Succeeded;
	}

	public async Task<IEnumerable<RoleDto?>> GetRoles()
	{
		var roles = await roleManager.Roles.ToListAsync();

		var roleDtos = roles.Select(async r => await MapRoleAsync(r));

		return await Task.WhenAll(roleDtos);
	}

	public async Task<RoleDto?> GetRole(string id)
	{
		var IdentityRole = await roleManager.FindByIdAsync(id);

		if (IdentityRole == null) return null;

		return await MapRoleAsync(IdentityRole);
	}

	public async Task<IEnumerable<string>> GetClaims(string name)
	{
		var IdentityRole = await roleManager.FindByNameAsync(name);

		if (IdentityRole == null) return new List<string>();

		var claims = await roleManager.GetClaimsAsync(IdentityRole);

		return claims.Select(c => c.Value);
	}

	public async Task<bool> UpdateRole(RoleDto roleDto)
	{
		var IdentityRole = await roleManager.FindByIdAsync(roleDto.Id);

		if (IdentityRole == null) return false;

		IdentityRole.Name = roleDto.Name;

		var result = await roleManager.UpdateAsync(IdentityRole);

		if (!result.Succeeded) return false;

		var existingClaims = await roleManager.GetClaimsAsync(IdentityRole);

		// Remove claims
		foreach (var claim in existingClaims)
		{
			if (roleDto.Claims.Contains(claim.Value)) continue;

			await roleManager.RemoveClaimAsync(IdentityRole, claim);
		}

		// Add new claims
		foreach (var claim in roleDto.Claims)
		{
			if (existingClaims.Any(c => c.Value == claim)) continue;

			await roleManager.AddClaimAsync(IdentityRole, new Claim("Permission", claim));
		}

		return true;
	}

	private async Task<RoleDto> MapRoleAsync(IdentityRole identityRole)
	{
		var roleDto = mapper.Map<RoleDto>(identityRole);

		var claims = await roleManager.GetClaimsAsync(identityRole);

		roleDto.Claims = claims.Select(c => c.Value).ToList();

		return roleDto;
	}
}
