namespace Microsoft.Extensions.DependencyInjection;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddJwtAuthentication(this WebApplicationBuilder builder)
    {
        builder.Services.AddJwtAuthentication(builder.Configuration);
        return builder;
    }
    public static WebApplicationBuilder AddJwtAuthentication(this WebApplicationBuilder builder, Action<JwtOptions> configuration)
    {
        builder.Services.AddJwtAuthentication(configuration);
        return builder;
    }
    public static WebApplicationBuilder AddJwtAuthentication(this WebApplicationBuilder builder, JwtEvents events)
    {
        builder.Services.AddJwtAuthentication(builder.Configuration, events);
        return builder;
    }
    public static WebApplicationBuilder AddJwtAuthentication(this WebApplicationBuilder builder, Action<AuthorizationOptions> authOptions)
    {
        builder.Services.AddJwtAuthentication(builder.Configuration, authOptions);
        return builder;
    }
    public static WebApplicationBuilder AddJwtAuthentication(this WebApplicationBuilder builder, Action<AuthorizationOptions> authOptions, JwtEvents events)
    {
        builder.AddJwtAuthentication(options => builder.Configuration.GetSection(JwtOptions.SectionKey).Bind(options), authOptions, events);
        return builder;
    }
    public static WebApplicationBuilder AddJwtAuthentication(this WebApplicationBuilder builder, Action<JwtOptions> configuration, Action<AuthorizationOptions> authOptions, JwtEvents events)
    {
        builder.Services.AddJwtAuthentication(configuration, authOptions, events);
        return builder;
    }
}
