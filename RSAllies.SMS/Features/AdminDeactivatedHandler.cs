using MediatR;
using RSAllies.Shared.Notifications;
using RSAllies.SMS.Contracts.Requests;
using RSAllies.SMS.Services;

namespace RSAllies.SMS.Features
{
    internal class AdminDeactivatedHandler(MessageService messageService) : INotificationHandler<AdminDeactivated>
    {
        public async Task Handle(AdminDeactivated notification, CancellationToken cancellationToken)
        {
            var phone = 255 + notification.Phone.TrimStart('0');

            var message = $"Hello, Your account has been deactivated at Site Administration " +
                          $"your username is {notification.Username} and " +
                          $"your password will not changed " +
                          $"Do not share your credentials ";

            var sms = new Sms
            {
                source_addr = "RSAllies",
                schedule_time = string.Empty,
                encoding = "0",
                message = message,
                recipients = new List<Recipient>
                {
                    new Recipient { recipient_id = "1", dest_addr = phone}
                }
            };

            await messageService.SendMessage(sms);
        }
    }
}
