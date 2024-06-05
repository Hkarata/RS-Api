using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RSAllies.Venues.Data;
using RSAllies.Venues.Notifications;

namespace RSAllies.Venues.Notifications
{
    internal class BookingMade(Guid sessionId) : INotification
    {
        public Guid SessionId { get; } = sessionId;
    }
}

internal class BookingMadeHandler(VenueDbContext context, ILogger<BookingMadeHandler> logger) : INotificationHandler<BookingMade>
{
    public async Task Handle(BookingMade notification, CancellationToken cancellationToken)
    {
        var session = await context.Sessions
            .Where(s => s.Id == notification.SessionId)
            .SingleOrDefaultAsync(cancellationToken);

        session!.Capacity++;

        await context.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Session {}'c capacity has been incremented, its capacity is {Capacity}", notification.SessionId, session.Capacity);
    }
}
