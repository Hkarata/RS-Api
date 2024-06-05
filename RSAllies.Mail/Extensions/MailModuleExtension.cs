using MailKit.Net.Smtp;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RSAllies.Mail.Data;
using System.Reflection;

namespace RSAllies.Mail.Extensions
{
    public static class MailModuleExtension
    {
        public static IServiceCollection AddMailModuleServices(
            this IServiceCollection services,
            ConfigurationManager configurationManager,
            List<Assembly> mediatRAssemblies
            )
        {
            // MediatR Registration
            mediatRAssemblies.Add(typeof(MailModuleExtension).Assembly);

            // DbContext Registration
            services.AddDbContext<EmailDbContext>(options =>
            {
                options.UseSqlServer(configurationManager.GetConnectionString("AppDbConnection"));
            });

            services.AddSingleton<SmtpClient>(options =>
            {
                var smtpClient = new SmtpClient();
                smtpClient.Connect("mail.privateemail.com", 465, true);
                smtpClient.Authenticate("donotreply@roadsafetyallies.me", "Hmkmkombe2.");

                return smtpClient;
            });

            return services;
        }
    }
}
