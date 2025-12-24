namespace Microsoft.Extensions.DependencyInjection;

public static partial class ServiceCollectionExtensions
{
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration,
        Action<AuthorizationOptions> authOptions)
    {
        services.AddJwtAuthentication(options => configuration.GetSection(JwtOptions.SectionKey).Bind(options), authOptions);
        return services;
    }

    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, Action<JwtOptions> configuration,
        Action<AuthorizationOptions> authOptions)
    {
        services.AddJwtAuthentication(configuration, authOptions, null);
        return services;
    }
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services,
        Action<AuthorizationOptions> authOptions)
    {
        services.AddJwtAuthentication(null, authOptions, null);
        return services;
    }
}
