using Microsoft.Extensions.Logging;
using RSAllies.Venues.Contracts.Requests;

namespace RSAllies.Venues.Extensions
{
    public static partial class Logging
    {
        [LoggerMessage(Level = LogLevel.Information, Message = "Venue created")]
        public static partial void LogVenueCreated(this ILogger logger, [LogProperties] CreateVenueDto venue);

        [LoggerMessage(LogLevel.Information, "Session created")]
        public static partial void LogSessionCreated(this ILogger logger, [LogProperties] CreateSessionDto session);

        [LoggerMessage(LogLevel.Information, "Booking Created")]
        public static partial void LogBookingCreated(this ILogger logger, [LogProperties] CreateBookingDto booking);

    }
}
