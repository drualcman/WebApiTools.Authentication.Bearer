namespace Microsoft.Extensions.DependencyInjection;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddJwtAuthentication(this WebApplicationBuilder builder, JwtEvents events = null)
    {
        builder.Services.AddJwtAuthentication(builder.Configuration, events);
        return builder;
    }
}
