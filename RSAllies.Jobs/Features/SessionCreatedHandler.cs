using Hangfire;
using MediatR;
using RSAllies.Jobs.Jobs;
using RSAllies.Shared.Notifications;

namespace RSAllies.Jobs.Features
{
    internal class SessionCreatedHandler(IBackgroundJobClientV2 jobClient) : INotificationHandler<SessionCreated>
    {
        public Task Handle(SessionCreated notification, CancellationToken cancellationToken)
        {
            var time = notification.ScheduledAt.AddHours(-1);
            jobClient.Schedule<SessionJob>(x => x.ExecuteAsync(notification.SessionId, cancellationToken), time);
            return Task.CompletedTask;
        }
    }
}
