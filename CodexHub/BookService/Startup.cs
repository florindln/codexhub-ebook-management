using Amazon.S3;
using BookService.AppServices;
using BookService.Controllers;
using BookService.Data;
using BookService.Entities;
using CodexhubCommon;
using CodexhubCommon.MassTransit;
using CodexhubCommon.Settings;
using GreenPipes;
using MassTransit;
using MassTransit.Definition;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace BookService
{
    public class Startup
    {
        private ServiceSettings serviceSettings;
        private bool InDocker { get { return Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true"; } }


        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            serviceSettings = Configuration.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>();

            services.AddMassTransitWithRabbitMq(InDocker, "guest", "guest");

            services.AddDbContext<BookDbContext>(options =>
            {
                var mysqlConnectionString = Configuration.GetConnectionString("MySql");
                var serverVersion = new MySqlServerVersion(new Version(8, 0, 27));
                options.UseMySql(mysqlConnectionString, serverVersion);
            });

            var redis = ConnectionMultiplexer.Connect(Configuration.GetConnectionString("Redis"));
            services.AddScoped<IDatabase>(s => redis.GetDatabase());

            services.AddScoped<BookApp>();
            services.AddScoped<IBookRepository, BookRepository>();
            //services.AddAWSService<IAmazonS3>();

            //var options = Configuration.GetAWSOptions();
            //IAmazonS3 client = options.CreateServiceClient<IAmazonS3>();

            services.AddDefaultAWSOptions(Configuration.GetAWSOptions());
            services.AddAWSService<IAmazonS3>();

            services.AddSingleton<ILogger>(svc => svc.GetRequiredService<ILogger<BooksController>>());
            services.AddControllers().AddNewtonsoftJson();
            services.AddSwaggerGen();

            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, BookDbContext bookDbContext)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            if (env.IsDevelopment())
            {
                app.UseExceptionHandler("/error-development");
            }
            else
            {
                app.UseExceptionHandler("/error");
            }

            //bookDbContext.Database.Migrate();

            app.UseCors(
      options => options.WithOrigins("http://localhost:3009").AllowAnyMethod());

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger();

            app.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint("/swagger/v1/swagger.json", "BookApi");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
