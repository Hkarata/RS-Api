﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

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
            return services;
        }
    }
}