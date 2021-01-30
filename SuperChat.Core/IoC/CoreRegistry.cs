using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SuperChat.Core.ConfigModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperChat.Core.IoC
{
    public static class CoreRegistry
    {
        public static void AddCoreRegistry(this IServiceCollection services)
        {
            services.AddSingleton((serviceProvider) =>
            {
                var configuration = serviceProvider.GetService<IConfiguration>();
                var settings = configuration.GetSection("AuthenticationConfig").Get<AuthenticationSettings>();
                return settings;
            });
        }
    }
}
