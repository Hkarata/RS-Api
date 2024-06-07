using MediatR;
using RSAllies.Shared.Notifications;
using RSAllies.SMS.Contracts.Requests;
using RSAllies.SMS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSAllies.SMS.Features
{
    internal class AdminCreatedHandler(MessageService messageService) : INotificationHandler<AdminCreated>
    {
        public async Task Handle(AdminCreated notification, CancellationToken cancellationToken)
        {
            var phone = 255 + notification.Phone.TrimStart('0');

            var message = $"Hello, Join us at Site Administration " +
                          $"your username is {notification.Username} and " +
                          $"your password is {notification.Password}. " +
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
