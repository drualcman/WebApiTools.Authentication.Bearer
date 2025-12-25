namespace WebApiTools.Authentication.Bearer.Helpers;

internal static class EventsHelper
{
    public static JwtBearerEvents ApplyTo(JwtBearerEvents targetEvents, JwtEvents eventsConfig)
    {
        if (eventsConfig?.OnMessageReceived != null)
            targetEvents.OnMessageReceived = eventsConfig.OnMessageReceived;

        if (eventsConfig?.OnAuthenticationFailed != null)
            targetEvents.OnAuthenticationFailed = eventsConfig.OnAuthenticationFailed;

        if (eventsConfig?.OnChallenge != null)
            targetEvents.OnChallenge = eventsConfig.OnChallenge;

        if (eventsConfig?.OnTokenValidated != null)
            targetEvents.OnTokenValidated = eventsConfig.OnTokenValidated;

        if (eventsConfig?.OnForbidden != null)
            targetEvents.OnForbidden = eventsConfig.OnForbidden;

        return targetEvents;
    }
}
