using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RSAllies.Mail.Data;

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

            return services;
        }
    }
}
