using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;

namespace InterviewMaster
{
    public class Program
    {
        public static string AppSettingsPath { get; set; }
        public Program()
        {
            AppSettingsPath = "appsettings.json";
        }

        public static Action<IConfigurationBuilder?> AddCustomConfig;
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
