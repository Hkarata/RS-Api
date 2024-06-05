using Hangfire;
using MailKit.Net.Smtp;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RSAllies.Jobs.Data;
using RSAllies.Jobs.Jobs;
using RSAllies.Jobs.Services;
using System.Reflection;

namespace RSAllies.Jobs.Extensions
{
    public static class JobsModuleExtension
    {
        public static IServiceCollection AddJobsModuleServices(
            this IServiceCollection services,
            ConfigurationManager configurationManager,
            List<Assembly> mediatRAssemblies
            )
        {
            // MediatR Registration
            mediatRAssemblies.Add(typeof(JobsModuleExtension).Assembly);

            // Hangfire Registration
            services.AddHangfire(config =>
            {
                config.SetDataCompatibilityLevel(CompatibilityLevel.Version_180);
                config.UseSimpleAssemblyNameTypeSerializer();
                config.UseRecommendedSerializerSettings();
                config.UseSqlServerStorage(configurationManager.GetConnectionString("AppDbConnection"));
            });

            // Hangfire Server Registration
            services.AddHangfireServer(options =>
            {
                options.SchedulePollingInterval = TimeSpan.FromSeconds(15);
            });

            services.AddDbContext<JobsDbContext>(options =>
            {
                options.UseSqlServer(configurationManager.GetConnectionString("AppDbConnection"));
            });

            services.AddScoped<DatabaseService>();

            services.AddScoped<SessionJob>();

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
