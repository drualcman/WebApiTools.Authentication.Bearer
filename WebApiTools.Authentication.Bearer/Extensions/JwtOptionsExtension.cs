namespace WebApiTools.Authentication.Bearer.Extensions;

internal static class JwtOptionsExtension
{
    internal static TokenValidationParameters ToTokenValidationParameters(this JwtOptions options)
    {
        return new TokenValidationParameters
        {
            ValidateIssuer = options.ValidateIssuer,
            ValidateAudience = options.ValidateAudience,
            ValidateLifetime = options.ValidateLifetime,
            ValidateIssuerSigningKey = options.ValidateIssuerSigningKey,
            ValidIssuer = options.Issuer,
            ValidAudience = options.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SecretKey))
        };
    }
}
