namespace Microsoft.Extensions.DependencyInjection;
public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddJwtAuthentication(this WebApplicationBuilder builder)
    {
        builder.Services.AddJwtAuthentication(builder.Configuration);
        return builder;
    }
}
