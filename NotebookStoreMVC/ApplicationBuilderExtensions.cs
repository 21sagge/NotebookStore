using Microsoft.CodeAnalysis.CSharp.Syntax;

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
}
