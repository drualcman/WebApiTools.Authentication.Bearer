[![Nuget](https://img.shields.io/nuget/v/WebApiTools.Authentication.Bearer?style=for-the-badge)](https://www.nuget.org/packages/WebApiTools.Authentication.Bearer)
[![Nuget](https://img.shields.io/nuget/dt/WebApiTools.Authentication.Bearer?style=for-the-badge)](https://www.nuget.org/packages/WebApiTools.Authentication.Bearer)
[![License](https://img.shields.io/badge/license-MIT-blue.svg?style=for-the-badge)](https://opensource.org/licenses/MIT)

# WebApiTools.Authentication.Bearer

This library provides extensions to easily configure **JWT Bearer Authentication** in your ASP.NET Core applications.

### Installation
```bash
dotnet add package WebApiTools.Authentication.Bearer
```

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

## 4. Advanced Usage with Custom Events 
You can now handle JWT Bearer events for advanced scenarios:
```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddJwtAuthentication(options =>
{
    options.Issuer = "MyIssuer";
    options.Audience = "MyAudience";
    options.SecretKey = "SuperSecretKey123456";
    options.ExpirationInDays = 7;
}, new JwtEvents        // all are optionals, so only one can be used
{
    OnMessageReceived = async (context) =>
    {
        // Custom token extraction logic
        var token = context.Request.Headers["X-Custom-Token"].FirstOrDefault();
        if (!string.IsNullOrEmpty(token))
        {
            context.Token = token;
        }
        await Task.CompletedTask;
    },
    
    OnAuthenticationFailed = async (context) =>
    {
        // Custom error handling
        Console.WriteLine($"Authentication failed: {context.Exception.Message}");
        await Task.CompletedTask;
    },
    
    OnTokenValidated = async (context) =>
    {
        // Add custom claims or validation
        var claimsIdentity = context.Principal.Identity as ClaimsIdentity;
        claimsIdentity?.AddClaim(new Claim("CustomClaim", "CustomValue"));
        await Task.CompletedTask;
    },
    
    OnChallenge = async (context) =>
    {
        // Custom challenge response
        context.Response.Headers.Append("X-Custom-Challenge", "Bearer error=\"invalid_token\"");
        await Task.CompletedTask;
    },
    
    OnForbidden = async (context) =>
    {
        // Custom forbidden response
        context.Response.StatusCode = 403;
        await context.Response.WriteAsync("Custom forbidden message");
        await Task.CompletedTask;
    }
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

## JwtEvents Configuration

`JwtEvents` allows you to handle JWT Bearer authentication events:

- **OnMessageReceived**: Invoked when a token has been received.
- **OnAuthenticationFailed**: Invoked if authentication fails.
- **OnTokenValidated**: Invoked after the token has been validated.
- **OnChallenge**: Invoked before a challenge is sent.
- **OnForbidden**: Invoked if authorization fails and results in a forbidden response.

## Available Extension Methods
```csharp
// From builder simple
builder.AddJwtAuthentication();

// From builder custom events
builder.AddJwtAuthentication(JwtEvents events);

// custom configuration
// From IConfiguration
builder.Services.AddJwtAuthentication(IConfiguration configuration);

// With custom events
builder.Services.AddJwtAuthentication(JwtEvents events);

// With custom Authorization options
builder.Services.AddJwtAuthentication(Action<AuthorizationOptions> options);

// With manual options configuration
builder.Services.AddJwtAuthentication(Action<JwtOptions> configuration);

// With manual options configuration and authorization options
builder.Services.AddJwtAuthentication(Action<JwtOptions> configuration, Action<AuthorizationOptions> options);

// With manual options configuration and events
builder.Services.AddJwtAuthentication(Action<JwtOptions> configuration, JwtEvents events);

// With manual options configuration, authorization options and events
builder.Services.AddJwtAuthentication(Action<JwtOptions> configuration, Action<AuthorizationOptions> options, JwtEvents events);

// using authentication
...
app.UseJwtAuthentication();
...
app.MapGet("", () => Results.Ok("Hello world!")).RequireAuthorization();
```

## Best Practices

1. Always store `SecretKey` securely (use environment variables or secrets manager).
2. Place `app.UseJwtAuthentication()`; **before** mapping endpoints.
3. Use the simplest overload unless you need advanced customization.
4. For production, use **strong secret keys** and consider key rotation.
5. Use custom events for logging, custom token validation, or specialized error handling.

### Example appsettings.json
```json
{
  "JwtOptions": {
    "Issuer": "MyApp",
    "Audience": "MyAppUsers",
    "SecretKey": "YourSuperSecretKeyHereAtLeast32CharactersLong",
    "ExpirationInDays": 7,
    "ValidateIssuer": true,
    "ValidateAudience": true,
    "ValidateLifetime": true,
    "ValidateIssuerSigningKey": true
  }
}
```

### Dependencies

- Microsoft.AspNetCore.Authentication.JwtBearer

## Contributing

If you encounter issues or have suggestions for improvements, please submit an issue or pull request to the repository hosting this library.