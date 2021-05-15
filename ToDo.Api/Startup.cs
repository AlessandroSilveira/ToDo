using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ToDo.Infra.Context;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using ToDo.Api.DependencyInjections;
using Refit;
using ToDo.Domain.Services;

namespace ToDo.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.ConfigureRepositories();
            services.ConfigureHealthCheck(Configuration);
            services.ConfigureAuthentication(Configuration);
            services.ConfigureSwagger();
            services.ConfigureMediatr();

            services.AddDbContext<DataContext>(opt =>
                opt.UseSqlServer(Configuration.GetConnectionString("ConnectionStrings")));

            services.AddDistributedRedisCache(options =>
            {
                options.Configuration = Configuration.GetConnectionString("ConexaoRedis");
                options.InstanceName = "ToDo.Api";
            });
            // var toDoBaseUrl = "http://localhost:5000";
            //
            // services.AddRefitClient<IExampleGetToDoService>()
            //     .ConfigureHttpClient(c => c.BaseAddress = new System.Uri(toDoBaseUrl))
            //     .ConfigurePrimaryHttpMessageHandler(() => NoSslValidationHandler);
            //
            // System.Net.ServicePointManager.ServerCertificateValidationCallback +=
            //     delegate(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate,
            //         System.Security.Cryptography.X509Certificates.X509Chain chain,
            //         System.Net.Security.SslPolicyErrors sslPolicyErrors)
            //     {
            //         return true; // **** Always accept
            //     };
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ASP.NET 5 Web API v1"));
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecksUI();
                endpoints.MapHealthChecks("/health", new HealthCheckOptions()
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
            });
        }
    }
}