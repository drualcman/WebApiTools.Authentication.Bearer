namespace Microsoft.Extensions.DependencyInjection;

public static partial class ServiceCollectionExtensions
{
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddJwtAuthentication(options => configuration.GetSection(JwtOptions.SectionKey).Bind(options), null, null);
        return services;
    }
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, Action<JwtOptions> configuration)
    {
        services.AddJwtAuthentication(configuration, null, null);
        return services;
    }
}
