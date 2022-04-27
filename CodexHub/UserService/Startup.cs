using CodexhubCommon.MassTransit;
using CodexhubCommon.MongoDB;
using CodexhubCommon.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using UserService.Auth;
using UserService.Entities;

namespace UserService
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
            //services.AddMassTransitWithRabbitMq();

            services.AddMongo().AddMongoRepository<UserEntity>("users");
            services.AddSingleton<JwtAuthenticationManager>();

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("MyUltraSuperSecretKeyWhichIsNotHardCoded")),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };
            });

            services.AddControllers();
            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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


            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwagger();

            app.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint("/swagger/v1/swagger.json", "UserApi");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
