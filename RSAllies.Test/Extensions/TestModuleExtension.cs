using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RSAllies.Test.Data;
using System.Reflection;

namespace RSAllies.Test.Extensions
{
    public static class TestModuleExtension
    {
        public static IServiceCollection AddTestModuleServices(
            this IServiceCollection services,
            ConfigurationManager configurationManager,
            List<Assembly> mediatRAssemblies
            )
        {
            // Database Service Registration
            services.AddDbContext<TestDbContext>(options =>
                options.UseSqlServer(configurationManager.GetConnectionString("AppDbConnection")));

            // MediatR Registration
            mediatRAssemblies.Add(typeof(TestModuleExtension).Assembly);

            return services;
        }
    }
}
