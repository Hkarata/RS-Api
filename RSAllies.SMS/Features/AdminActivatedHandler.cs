using MediatR;
using RSAllies.Shared.Notifications;
using RSAllies.SMS.Contracts.Requests;
using RSAllies.SMS.Services;

namespace RSAllies.SMS.Features
{
    internal class AdminActivatedHandler(MessageService messageService) : INotificationHandler<AdminActivated>
    {
        public async Task Handle(AdminActivated notification, CancellationToken cancellationToken)
        {
            var phone = 255 + notification.Phone.TrimStart('0');

            var message = $"Hello, Your account has been activated at Site Administration " +
                          $"your username is {notification.Username} and " +
                          $"your password has not changed " +
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
