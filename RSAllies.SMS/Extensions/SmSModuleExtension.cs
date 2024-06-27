using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RSAllies.SMS.Data;
using RSAllies.SMS.Services;
using System.Reflection;

namespace RSAllies.SMS.Extensions;

public static class SmSModuleExtension
{

    public static IServiceCollection AddSmsModuleServices(
        this IServiceCollection services,
        ConfigurationManager configurationManager,
        List<Assembly> mediatRAssemblies
        )
    {

        services.AddDbContext<SmsDbContext>(options =>
        {
            options.UseSqlServer(configurationManager.GetConnectionString("AppDbConnection"));
        });

        mediatRAssemblies.Add(typeof(SmSModuleExtension).Assembly);

        services.AddSingleton<MessageService>();

        return services;
    }

}