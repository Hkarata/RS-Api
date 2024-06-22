using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RSAllies.Test.Data;
using RSAllies.Test.Messaging;
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

            services.AddMassTransit(options =>
            {
                options.AddConsumer<ResponseConsumer>();
                options.UsingInMemory((context, cfg) =>
                {
                    cfg.ConfigureEndpoints(context);
                });

            });

            return services;
        }
    }
}
