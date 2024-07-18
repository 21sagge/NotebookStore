
using System.Reflection;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace NotebookStore.Business;

internal class RoleService : IRoleService
{
	private readonly RoleManager<IdentityRole> roleManager;

	public RoleService(RoleManager<IdentityRole> _roleManager)
	{
		roleManager = _roleManager;
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

	public async Task<IEnumerable<string?>> GetRoles()
	{
		return await roleManager.Roles.Select(r => r.Name).ToListAsync();
	}

	public async Task<string?> GetRole(string role)
	{
		var IdentityRole = await roleManager.FindByNameAsync(role);

		return IdentityRole?.Name;
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

	public async Task<IEnumerable<string>> GetAllClaims()
	{
		var allRoles = await roleManager.Roles.ToListAsync();
		var allClaims = new List<Claim>();

		foreach (var role in allRoles)
		{
			var claims = await roleManager.GetClaimsAsync(role);
			allClaims.AddRange(claims);
		}

		var distinctClaims = allClaims.Select(c => c.Value).Distinct();

		return distinctClaims;
	}
}
