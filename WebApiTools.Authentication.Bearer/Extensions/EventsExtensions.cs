namespace Microsoft.Extensions.DependencyInjection;

public static partial class ServiceCollectionExtensions
{
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration, JwtEvents events)
    {
        services.AddJwtAuthentication(options => configuration.GetSection(JwtOptions.SectionKey).Bind(options), events);
        return services;
    }

    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, Action<JwtOptions> configuration,
        JwtEvents events)
    {
        services.AddJwtAuthentication(configuration, null, events);
        return services;
    }

    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services,
        JwtEvents events)
    {
        services.AddJwtAuthentication(null, null, events);
        return services;
    }
}
