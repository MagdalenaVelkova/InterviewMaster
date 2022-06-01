using InterviewMaster.Persistance.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace InterviewMaster.Test.Util
{
    public class InterviewMasterTestApp : IDisposable
    {
        private readonly IServiceScope serviceScope;

        private bool disposed;
        public IServiceProvider Services { get; }

        public InterviewMasterTestApp(IHost host)
        {
            serviceScope = host.Services.CreateScope();
            Services = serviceScope.ServiceProvider;
        }

        public static InterviewMasterTestApp BuildApp()
        {
            //api.appsettings.json
            Program.AppSettingsPath = "test.appsettings.json";
            Startup.RegisterOverrides = RegisterOverrides;

            var host = Program.CreateHostBuilder(new string[] { }).Build();
            return new InterviewMasterTestApp(host);
        }

        private static void RegisterOverrides(IServiceCollection serviceCollection)
        {
            serviceCollection
                    .AddSingleton<MongoDbService>()
                    .AddSingleton(service => service.GetService<MongoDbService>()!.MongoDatabase)
                    .AddSingleton<IIdGenerator, TestIdGenerator>()
                    .AddSingleton<TestIdGenerator>(x => (TestIdGenerator)x.GetService<IIdGenerator>());
        }

        //private static void AddCustomConfig(IConfigurationBuilder builder)
        //{
        //DotEnv.Config(true, "test.env")
        //}
        public void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }

            if (disposing)
            {
                TearDown();
            }

            disposed = true;
        }

        public T GetService<T>()
        {
            return Services.GetService<T>();
        }

        public void TearDown()
        {
            serviceScope?.Dispose();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
