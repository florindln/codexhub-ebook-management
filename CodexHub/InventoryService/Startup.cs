using CodexhubCommon.MassTransit;
using CodexhubCommon.MongoDB;
using InventoryService.Clients;
using InventoryService.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Timeout;
using System;
using System.Net.Http;

namespace InventoryService
{
    public class Startup
    {
        private const string AllowedOriginSetting = "AllowedOrigin";
        private bool InDocker { get { return Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true"; } }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMongo()
                .AddMongoRepository<BookUserEntity>("bookuser")
                .AddMongoRepository<CatalogBook>("catalogbooks")
                .AddMongoRepository<CatalogUser>("catalogusers")
                .AddMongoRepository<Rating>("ratings");


            //services.AddMassTransitWithRabbitMq();
            services.AddMassTransitWithRabbitMq(InDocker, "guest", "guest");


            //AddBookClient(services);

            services.AddControllers().AddNewtonsoftJson().AddJsonOptions(
        options => options.JsonSerializerOptions.PropertyNamingPolicy = null); ;
            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseCors(builder =>
                {
                    builder.WithOrigins(Configuration[AllowedOriginSetting])
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger();

            app.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint("/swagger/v1/swagger.json", "InventoryApi");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        private static void AddBookClient(IServiceCollection services)
        {
            services.AddHttpClient<BookCatalogClient>(client =>
            {
                client.BaseAddress = new Uri("http://localhost:5002");
            })
                            .AddTransientHttpErrorPolicy(builder => builder.Or<TimeoutRejectedException>().WaitAndRetryAsync(
                                5,
                                retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                                onRetry: (outcome, timespan, retryAttempt) =>
                                {
                                    var serviceProvider = services.BuildServiceProvider();
                                    serviceProvider.GetService<ILogger<BookCatalogClient>>()?
                                    .LogWarning($"Delaying for {timespan.TotalSeconds} seconds, then making retry {retryAttempt}");
                                }))
                            .AddTransientHttpErrorPolicy(builder => builder.Or<TimeoutRejectedException>().CircuitBreakerAsync(
                                3,
                                TimeSpan.FromSeconds(15),
                                onBreak: (outcome, timespan) =>
                                {
                                    var serviceProvider = services.BuildServiceProvider();
                                    serviceProvider.GetService<ILogger<BookCatalogClient>>()?
                                    .LogWarning($"Opening the circuit for {timespan.TotalSeconds} seconds");
                                },
                                onReset: () =>
                                {
                                    var serviceProvider = services.BuildServiceProvider();
                                    serviceProvider.GetService<ILogger<BookCatalogClient>>()?
                                    .LogWarning($"Closing the circuit");
                                }))
                            .AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(1));
        }
    }
}
