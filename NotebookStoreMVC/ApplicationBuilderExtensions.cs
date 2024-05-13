using Microsoft.AspNetCore.Identity;

namespace NotebookStoreMVC;

public static class ApplicationBuilderExtensions
{
  public static IApplicationBuilder UseDeveloper(this IApplicationBuilder app, string developer)
  {
    app.Use(async (context, next) =>
    {
      context.Response.Headers.Add($"X-Developer-{developer}", developer);
      await next.Invoke();
    });
    return app;
  }

  public static IApplicationBuilder AddRoles(this IApplicationBuilder app)
  {
    app.Use(async (context, next) =>
    {
      var roleManager = context.RequestServices.GetRequiredService<RoleManager<IdentityRole>>();
      var roles = new string[] { "Admin", "Editor", "Viewer" };

      foreach (var role in roles)
      {
        var roleExists = await roleManager.RoleExistsAsync(role);

        if (!roleExists)
        {
          await roleManager.CreateAsync(new IdentityRole(role));
        }
      }

      await next.Invoke();
    });

    return app;
  }
}
