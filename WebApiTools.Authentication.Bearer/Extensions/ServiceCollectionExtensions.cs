namespace Microsoft.Extensions.DependencyInjection;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddJwtAuthentication(options => configuration.GetSection(JwtOptions.SectionKey).Bind(options));
        return services;
    }

    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, Action<JwtOptions> configuration)
    {
        services.Configure(configuration);
        services.AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", options =>
            {
                JwtOptions jwtOptions = new();
                configuration(jwtOptions);
                options.TokenValidationParameters = jwtOptions.ToTokenValidationParameters();
            });
        services.AddAuthorization();
        return services;
    }
}
