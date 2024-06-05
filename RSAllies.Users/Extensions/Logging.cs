using Microsoft.Extensions.Logging;
using RSAllies.Shared.Requests;
using RSAllies.Users.Contracts.Requests;

namespace RSAllies.Users.Extensions
{
    public static partial class Logging
    {
        [LoggerMessage(LogLevel.Information, "User created")]
        public static partial void LogUserCreated(this ILogger logger, [LogProperties] CreateUserDto user);

        [LoggerMessage(LogLevel.Information, "User Authenticated")]
        public static partial void LogUserAuthenticated(this ILogger logger, [LogProperties] AuthenticationDto user);

        [LoggerMessage(LogLevel.Error, "User Authentication failed")]
        public static partial void LogUserAuthenticationFailed(this ILogger logger, [LogProperties] AuthenticationDto user);

    }
}
