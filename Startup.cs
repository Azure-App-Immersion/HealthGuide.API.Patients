using HealthGuide.API.Patients.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MySQL.Data.Entity.Extensions;
using Swashbuckle.AspNetCore.Swagger;

namespace HealthGuide.API.Patients
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)              
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<PatientsContext>(options => 
            {
                options.UseMySQL(Configuration["ConnectionStrings:MySql"]);
                options.EnableSensitiveDataLogging();
            });
            services.AddCors();
            services.AddMvc();
            services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("1.0.0", new Info { Title = "HealthGuide Patients API", Version = "1.0.0" });
                }
            );
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseCors(options => {
                options
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
            app.UseMvcWithDefaultRoute();
            app.UseStaticFiles();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/1.0.0/swagger.json", "HealthGuide Patients API 1.0.0");
            });

            var context = app.ApplicationServices.GetRequiredService<PatientsContext>();
            context.Database.EnsureCreated();
        }
    }
}