using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace WebApiTools.Authentication.Bearer.Options;

public class JwtEvents
{
    public Func<MessageReceivedContext, Task>? OnMessageReceived { get; set; }
    public Func<TokenValidatedContext, Task>? OnTokenValidated { get; set; }
    public Func<AuthenticationFailedContext, Task>? OnAuthenticationFailed { get; set; }
    public Func<JwtBearerChallengeContext, Task>? OnChallenge { get; set; }
    public Func<ForbiddenContext, Task>? OnForbidden { get; set; }
}
