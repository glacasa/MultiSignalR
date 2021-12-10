using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MultiSignalR.Common;
using System;
using System.IO;


var builder = new HostBuilder()
    .UseContentRoot(Directory.GetCurrentDirectory())
    .ConfigureHostConfiguration(builder =>
    {
        builder.AddCommandLine(args);
    })
    .ConfigureAppConfiguration((ctx, builder) =>
    {
        builder.AddJsonFile("appsettings.json")
               .AddJsonFile($"appsettings.{ctx.HostingEnvironment.EnvironmentName}.json", optional: true);

        if (ctx.HostingEnvironment.IsDevelopment())
        {
            builder.AddUserSecrets<Program>();
        }
    })
    .ConfigureLogging((ctx, logging) =>
    {
        logging.AddConsole();
    })
    .ConfigureServices((ctx, services) =>
    {
        services.AddCustomSignalR(ctx.Configuration.GetConnectionString("Redis"));
    })
    .UseConsoleLifetime();

var host = builder.Build();

using (var servicesScope = host.Services.CreateScope())
{
    var messageHub = servicesScope.ServiceProvider.GetService<MessageHubContextWrapper>();

    string msg ;

    while (true)
    {
        msg = Console.ReadLine();

        if (string.IsNullOrEmpty(msg))
        {
            return;
        }

        messageHub.SendMessage(msg);
    }


}