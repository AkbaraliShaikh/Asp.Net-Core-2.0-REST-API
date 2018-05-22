﻿using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Rapsody.Api
{
    public class Program
    {
        public static void Main(string[] args) => BuildWebHost(args).Run();

        public static IWebHost BuildWebHost(string[] args)
        {
            var config = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("hosting.json", optional: true)
                        .AddJsonFile("appsettings.json", optional: true)
                        .AddCommandLine(args)
                        .Build();

            return WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseConfiguration(config)
                .Build();
        }
    }
}
