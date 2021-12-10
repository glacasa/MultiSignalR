using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiSignalR.Common
{
    public static class SignalRDependencyInjection
    {
        public static void AddCustomSignalR(this IServiceCollection services, string redisConnectionString)
        {
            // Pour utiliser Redis sur un poste de dev avec docker :
            // docker run --name wancom-redis -p 6379:6379 -d redis 
            services.AddSignalR().AddStackExchangeRedis(redisConnectionString);

            services.AddScoped<MessageHubContextWrapper>();
        }
    }
}
