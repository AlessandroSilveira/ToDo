using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ToDo.Domain.Base;
using ToDo.Domain.Behaviors;
using ToDo.Domain.Commands;
using ToDo.Domain.Entities;
using ToDo.Domain.Handlers;
using ToDo.Domain.Notification;
using ToDo.Domain.Repositories.Interfaces;
using ToDo.Domain.Services;
using ToDo.Domain.Services.Interfaces;
using ToDo.Domain.Validators.Commands;
using ToDo.Infra.Base;
using ToDo.Infra.Context;
using ToDo.Infra.Repositories;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using ToDo.Domain.HealthCheck;
using ToDo.Api.DependencyInjections;

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
            services.AddControllers();
            services.ConfigureRepositories();
            services.ConfigureHealthCheck(Configuration);
            services.ConfigureAuthentication(Configuration);
            services.ConfigureSwagger();
            services.ConfigureMediatr();

            services.AddDbContext<DataContext>(opt =>
                opt.UseSqlServer(Configuration["ConnectionStrings"]));
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