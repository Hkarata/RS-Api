using MediatR;
using RSAllies.Shared.Notifications;
using RSAllies.Users.Data;

namespace RSAllies.Users.Handlers
{
    internal class GetPhoneNumbersHandler(UsersDbContext context, IMediator mediator) : INotificationHandler<GetPhoneNumbers>
    {
        public async Task Handle(GetPhoneNumbers notification, CancellationToken cancellationToken)
        {
            var phoneNumbers = new List<string>();

            foreach (var userId in notification.UsersId)
            {
                var user = await context.Accounts.FindAsync(userId);

                if (user != null)
                {
                    var phoneNumber = "255" + user.Phone!.TrimStart('0');
                    phoneNumbers.Add(phoneNumber);
                }

            }

            await mediator.Publish(new ReceivePhoneNumbers(notification.SessionId, phoneNumbers), cancellationToken);
        }
    }
}
