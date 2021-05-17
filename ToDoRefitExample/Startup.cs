using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using ToDo.Domain.Behaviors;
using ToDo.Domain.Commands;
using ToDo.Domain.Entities;
using ToDo.Domain.Handlers.ToDoHandles;
using ToDo.Domain.Notification;
using ToDo.Domain.Repositories.Interfaces;
using ToDo.Infra.Context;
using ToDo.Infra.Repositories;

namespace ToDoRefitExample
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

            services.AddControllers();

            var assembly = AppDomain.CurrentDomain.Load("ToDoRefitExample");
            services.AddMediatR(assembly);
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationRequestBehavior<,>));
            services.AddScoped<IDomainNotificationContext, DomainNotificationContext>();
            services.AddTransient<IRequestHandler<GetAllToDoCommand, IEnumerable<TodoItem>>, GetAllToDoCommandHandler>();
            services.AddScoped<ITodoRepository, TodoRepository>();
            services.AddDbContext<DataContext>(opt =>
               opt.UseSqlServer(Configuration.GetConnectionString("ConnectionStrings")));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ToDoRefitExample", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ToDoRefitExample v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
