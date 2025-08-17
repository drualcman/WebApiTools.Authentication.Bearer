[![Nuget](https://img.shields.io/nuget/v/WebApiTools.Authentication.Bearer?style=for-the-badge)](https://www.nuget.org/packages/WebApiTools.Authentication.Bearer)
[![Nuget](https://img.shields.io/nuget/dt/WebApiTools.Authentication.Bearer?style=for-the-badge)](https://www.nuget.org/packages/WebApiTools.Authentication.Bearer)

# WebApiTools.Authentication.Bearer

This library provides extensions to easily configure JWT Bearer Authentication in your ASP.NET Core applications.

## 1. Quick Setup (Simplest)

If your configuration file contains a `JwtOptions` section, you can add authentication in one line:

```csharp
var builder = WebApplication.CreateBuilder(args);

builder.AddJwtAuthentication();

var app = builder.Build();
app.UseCors();
app.UseJwtAuthentication();
app.UseHttpsRedirection();
```

## 2. Using Configuration Section Explicitly

If you want to be explicit about where the configuration comes from:

```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddJwtAuthentication(builder.Configuration);

var app = builder.Build();
app.UseCors();
app.UseJwtAuthentication();
app.UseHttpsRedirection();
```

## 3. Full Customization (Manual Options Binding)

If you prefer to configure `JwtOptions` programmatically:

```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddJwtAuthentication(options =>
{
    options.Issuer = "MyIssuer";
    options.Audience = "MyAudience";
    options.SecretKey = "SuperSecretKey123456";
    options.ExpirationInDays = 7;
    options.ValidateIssuer = true;
    options.ValidateAudience = true;
    options.ValidateLifetime = true;
    options.ValidateIssuerSigningKey = true;
});

var app = builder.Build();
app.UseCors();
app.UseJwtAuthentication();
app.UseHttpsRedirection();
```

## JwtOptions Configuration

`JwtOptions` allows you to configure:

- **Issuer**: The expected token issuer.
- **Audience**: The expected token audience.
- **SecretKey**: The symmetric security key used for signing tokens.
- **ExpirationInDays**: Default token expiration.
- **ValidateIssuer**: Whether to validate the issuer.
- **ValidateAudience**: Whether to validate the audience.
- **ValidateLifetime**: Whether to check token expiration.
- **ValidateIssuerSigningKey**: Whether to validate the signing key.

## Best Practices

1. Always store `SecretKey` securely (use environment variables or secrets manager).
2. Place `app.UseJwtAuthentication();` **before** mapping endpoints.
3. Use the simplest overload unless you need advanced customization.