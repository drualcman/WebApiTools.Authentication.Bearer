namespace Microsoft.Extensions.DependencyInjection;
public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseJwtAuthentication(this IApplicationBuilder app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
        return app;
    }
}
