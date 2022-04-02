using InterviewMaster.Application.Services;
using InterviewMaster.Persistance.Extensions;
using InterviewMaster.Persistance.Repositories;
using InterviewMaster.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using System;

namespace InterviewMaster
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            //services.AddAuthentication(AzureADDefaults.BearerAuthenticationScheme)
            //   .AddAzureADBearer(options => Configuration.Bind("AzureAd", options));
            services.Configure<DatabaseSettings>(Configuration.GetSection("InterviewMasterDatabase"));

            services.AddSingleton<IMongoDatabase>(serviceProvider =>
            {
                var config = serviceProvider.GetService<IConfiguration>();
                var url = new MongoUrl(Configuration["InterviewMasterDatabase:ConnectionString"]);
                var settings = MongoClientSettings.FromUrl(url);
                var client = new MongoClient(settings);
                return client.GetDatabase(url.DatabaseName, new MongoDatabaseSettings());
            }
);
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Interview Master Docs", Version = "v1" });
            });
            services.AddScoped<IIdGenerator, IdGenerator>();
            services.AddScoped<IQuestionsService, QuestionsRepository>();
            services.AddScoped<IUserProfileService, UserProfileRepository>();
            services.AddScoped<IUserSolutionsService, UserSolutionsRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000"));
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Interview Master Docs v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}