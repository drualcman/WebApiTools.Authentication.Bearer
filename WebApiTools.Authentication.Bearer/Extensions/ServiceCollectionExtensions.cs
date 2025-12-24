namespace Microsoft.Extensions.DependencyInjection;

public static partial class ServiceCollectionExtensions
{
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services)
    {
        services.AddJwtAuthentication(null, null, null);
        return services;
    }

    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services,
        Action<JwtOptions> configuration,
        Action<AuthorizationOptions> authOptions,
        JwtEvents events)
    {
        if (configuration is not null)
            services.Configure(configuration);
        else
            services
                .AddOptions<JwtOptions>()
                .BindConfiguration(JwtOptions.SectionKey);

        services.AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", options =>
            {
                JwtOptions jwtOptions = new();
                configuration(jwtOptions);
                options.TokenValidationParameters = jwtOptions.ToTokenValidationParameters();
                if (events is not null)
                    options.Events = EventsHelper.Create(events);
            });
        if (authOptions is not null)
            services.AddAuthorization(authOptions);
        else
            services.AddAuthorization();
        return services;
    }
}
