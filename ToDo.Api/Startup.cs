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
using Todo.Domain.Handlers;
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

            services.AddDbContext<DataContext>(opt =>
                opt.UseSqlServer("Server=localhost;Database=Todos;User ID=sa;Password=Sprpwd1234;MultipleActiveResultSets=True;"));

          

            services.AddHealthChecks()
             .AddDiskStorageHealthCheck(s => s.AddDrive("C:\\", 1024))
                .AddProcessAllocatedMemoryHealthCheck(512)
                .AddProcessHealthCheck("ProcessName", p => p.Length > 0)
                .AddWindowsServiceHealthCheck("someservice", s => true)
                .AddUrlGroup(new Uri("https://localhost:44318/ToDo"), "Example endpoint")
                .AddSqlServer("Server=localhost;Database=Todos;User ID=sa;Password=Sprpwd1234;MultipleActiveResultSets=True;");

            services
               .AddHealthChecksUI(s =>
               {
                   s.AddHealthCheckEndpoint("endpoint1", "https://localhost:44318/health");
               })
               .AddInMemoryStorage();

            services.AddScoped<ITodoRepository, TodoRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITodoService, ToDoService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<TodoHandler, TodoHandler>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<DataContext>();
            services.AddScoped<IHealthCheck, SelfHealthCheck>();

            var key = Encoding.ASCII.GetBytes(Configuration["JWT:Secret"]);
            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            services.AddSwaggerGen(swagger =>
            {
                //This is to generate the Default UI of Swagger Documentation    
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "ASP.NET 5 Web API",
                    Description = "Authentication and Authorization in ASP.NET 5 with JWT and Swagger"
                });

                // To Enable authorization using Swagger (JWT)    
                swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description =
                        "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
                });
                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateTodoCommandValidator>());
            var assembly = AppDomain.CurrentDomain.Load("ToDo.Domain");
            services.AddMediatR(assembly);
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationRequestBehavior<,>));
            services.AddScoped<IDomainNotificationContext, DomainNotificationContext>();
            services.AddScoped<AsyncRequestHandler<CreateTodoCommand>, CreateTodoCommandHandler>();
            services.AddTransient<IRequestHandler<GetAllToDoCommand, IEnumerable<TodoItem>>, GetAllToDoCommandHandler>();

        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ASP.NET 5 Web API v1"));


            app.UseAuthentication();
            app.UseAuthorization();
            app.UseHttpsRedirection();
            app.UseRouting();

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