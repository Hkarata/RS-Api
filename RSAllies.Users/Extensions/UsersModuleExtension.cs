using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RSAllies.Users.Data;
using System.Reflection;

namespace RSAllies.Users.Extensions;

public static class UsersModuleExtension
{

    public static IServiceCollection AddUsersModuleServices(
        this IServiceCollection services,
        ConfigurationManager configurationManager,
        List<Assembly> mediatRAssemblies
        )
    {
        // Database Service Registration
        services.AddDbContext<UsersDbContext>(options =>
            options.UseSqlServer(configurationManager.GetConnectionString("AppDbConnection")));

        // MediatR Registration
        mediatRAssemblies.Add(typeof(UsersModuleExtension).Assembly);

        return services;
    }

}