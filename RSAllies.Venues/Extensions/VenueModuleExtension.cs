using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RSAllies.Venues.Data;
using System.Reflection;

namespace RSAllies.Venues.Extensions
{
    public static class VenueModuleExtension
    {
        public static IServiceCollection AddVenuesModuleServices(
            this IServiceCollection services,
            ConfigurationManager configurationManager,
            List<Assembly> mediatRAssemblies
            )
        {
            // Database Service Registration
            services.AddDbContext<VenueDbContext>(options =>
                options.UseSqlServer(configurationManager.GetConnectionString("AppDbConnection")));

            // MediatR Registration
            mediatRAssemblies.Add(typeof(VenueModuleExtension).Assembly);

            return services;
        }
    }
}
