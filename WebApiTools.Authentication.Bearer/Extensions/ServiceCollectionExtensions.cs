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
        Action<JwtOptions> jwtConfiguration = configuration ??
            (options => services
            .AddOptions<JwtOptions>()
            .BindConfiguration(JwtOptions.SectionKey));

        services.Configure(jwtConfiguration);

        services.AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", options =>
            {
                JwtOptions jwtOptions = new();
                jwtConfiguration(jwtOptions);
                options.TokenValidationParameters = jwtOptions.ToTokenValidationParameters();
                if (events is not null)
                {
                    options.Events ??= new JwtBearerEvents();
                    EventsHelper.ApplyTo(options.Events, events);
                }
            });

        if (authOptions is not null)
            services.AddAuthorization(authOptions);
        else
            services.AddAuthorization();

        return services;
    }
}
