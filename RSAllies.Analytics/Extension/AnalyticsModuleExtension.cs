using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RSAllies.Analytics.Data;

namespace RSAllies.Analytics.Extension
{
    public static class AnalyticsModuleExtension
    {
        public static IServiceCollection AddAnalyticsModule(
            this IServiceCollection services,
            ConfigurationManager configurationManager,
            List<Assembly> mediatRAssemblies
            )
        {
            services.AddDbContext<AnalyticsDbContext>(options =>
            {
                options.UseSqlServer(configurationManager.GetConnectionString("AppDbConnection"));
            });

            // MediatR Registration
            mediatRAssemblies.Add(typeof(AnalyticsModuleExtension).Assembly);

            return services;
        }
    }
}
