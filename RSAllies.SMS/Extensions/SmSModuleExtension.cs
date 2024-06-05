using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RSAllies.SMS.Data;
using RSAllies.SMS.Services;
using System.Reflection;
using System.Text;

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

        services.AddHttpClient<MessageService>(client =>
        {
            const string apiKey = "23ff38b59d83ee03";
            const string secretKey = "OWZkMDcyN2Y1NmFlNzU1OTFkNDhjZjJhNzhiMzE0OTZhMDljNGNkZDZkZGE2NmI1NjYwMjE0NTZmOGVmZWNiNg==";
            client.DefaultRequestHeaders.Add("api_key", apiKey);
            client.DefaultRequestHeaders.Add("secret_key", secretKey);
            client.BaseAddress = new Uri("https://apisms.beem.africa");
        });

        services.AddHttpClient<OtpService>(client =>
        {
            var key = "8a744d33c22c6111";
            var secret = "ZTAzOTE2ZjU1YzQ0Njk5MTA2NjVlYjE3ZDA2NTY5OWZjM2YwOGJiOTM5MWZmMjU4MGFmZjdjNzkwNGM3YTA3MQ==";
            var auth = Encoding.UTF8.GetBytes($"{key}:{secret}");
            client.DefaultRequestHeaders.Add("Authorization", $"Basic {Convert.ToBase64String(auth)}");
            client.BaseAddress = new Uri("https://apiotp.beem.africa");
        });

        return services;
    }

}