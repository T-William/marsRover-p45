﻿using System;
using MarsRover.API;
using MarsRover.API.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;


namespace marsrover.api
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<DataContext>();
                    context.Database.Migrate();
                }
                catch (System.Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "Migration Erros in Main");
                }
                host.Run();

            }
            Console.WriteLine("Hello World!");
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder =>
    {
        webBuilder.UseStartup<Startup>()
        // .UseUrls("http://localhost:5000") 
        // .UseUrls("http://localhost:5000", "http://192.168.27.177:5000")  
        .ConfigureKestrel((context, options) =>
        {
            options.AllowSynchronousIO = true;
        });
    });
    }
}
