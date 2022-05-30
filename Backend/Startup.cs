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
using Google.Apis.Auth.AspNetCore3;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;

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
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x=>
            {
                var Key = Encoding.ASCII.GetBytes(Configuration["Jwt:Key"]);
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Key)
                };
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Interview Master Docs", Version = "v1" });
            });
            services.AddScoped<IIdGenerator, IdGenerator>();
            services.AddScoped<IQuestionsRespository, QuestionsRepository>();
            services.AddScoped<IUserProfileRepository, UserProfileRepository>();
            services.AddScoped<IUserSolutionsRepository, UserSolutionsRepository>();
            services.AddScoped<IIdentityRepository, IdentityRepository>();
            services.AddScoped<IdentityService>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseCors(x => x.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod().WithMethods(new string[] { "POST", "PUT", "GET"}));
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Interview Master Docs v1"));
            }
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseHttpsRedirection();

            app.UseRouting();



            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
