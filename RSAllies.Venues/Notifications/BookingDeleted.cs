using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RSAllies.Venues.Data;
using RSAllies.Venues.Notifications;

namespace RSAllies.Venues.Notifications
{
    internal class BookingDeleted : INotification
    {
        public BookingDeleted(Guid sessionId)
        {
            SessionId = sessionId;
        }
        public Guid SessionId { get; }
    }
}

internal sealed class BookingDeletedHandler(VenueDbContext context, ILogger<BookingDeletedHandler> logger) : INotificationHandler<BookingDeleted>
{
    public async Task Handle(BookingDeleted notification, CancellationToken cancellationToken)
    {
        var session = await context.Sessions
            .Where(s => s.Id == notification.SessionId)
            .SingleOrDefaultAsync(cancellationToken);

        session!.Capacity--;

        await context.SaveChangesAsync();

        logger.LogInformation("Session {}'s capacity has been decremented, its capacity is {Capacity}", notification.SessionId, session.Capacity);
    }
}
