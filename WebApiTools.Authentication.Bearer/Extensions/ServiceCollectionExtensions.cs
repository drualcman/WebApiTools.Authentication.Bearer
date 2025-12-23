namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration, JwtEvents events = null)
    {
        services.AddJwtAuthentication(options => configuration.GetSection(JwtOptions.SectionKey).Bind(options), events);
        return services;
    }

    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, Action<JwtOptions> configuration, JwtEvents events = null)
    {
        services.Configure(configuration);
        services.AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", options =>
            {
                JwtOptions jwtOptions = new();
                configuration(jwtOptions);
                options.TokenValidationParameters = jwtOptions.ToTokenValidationParameters();
                if (events is not null)
                    options.Events = EventsHelper.Create(events);
            });
        services.AddAuthorization();
        return services;
    }
}
