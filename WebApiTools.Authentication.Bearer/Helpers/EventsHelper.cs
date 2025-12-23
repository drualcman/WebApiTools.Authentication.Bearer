namespace WebApiTools.Authentication.Bearer.Helpers;

internal static class EventsHelper
{
    public static JwtBearerEvents Create(
        JwtEvents eventsConfig)
    {
        return new JwtBearerEvents
        {
            OnMessageReceived = eventsConfig?.OnMessageReceived != null
                ? eventsConfig.OnMessageReceived
                : null,

            OnAuthenticationFailed = eventsConfig?.OnAuthenticationFailed != null
                ? eventsConfig.OnAuthenticationFailed
                : null,

            OnChallenge = eventsConfig?.OnChallenge != null
                ? eventsConfig.OnChallenge
                : null,

            OnTokenValidated = eventsConfig?.OnTokenValidated != null
                ? eventsConfig.OnTokenValidated
                : null,

            OnForbidden = eventsConfig?.OnForbidden != null
                ? eventsConfig.OnForbidden
                : null
        };
    }
}
