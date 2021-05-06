using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Text;
using ToDo.Domain.Auth;
using ToDo.Domain.Base;
using ToDo.Domain.Behaviors;
using ToDo.Domain.Commands;
using ToDo.Domain.Commands.AuthCommands;
using ToDo.Domain.Commands.ToDoCommands;
using ToDo.Domain.Commands.UserCommand;
using ToDo.Domain.Entities;
using ToDo.Domain.Handlers.AuthHandlers;
using ToDo.Domain.Handlers.ToDoHandles;
using ToDo.Domain.Handlers.UserHandlers;
using ToDo.Domain.HealthCheck;
using ToDo.Domain.Notification;
using ToDo.Domain.Repositories.Interfaces;
using ToDo.Domain.Services;
using ToDo.Domain.Services.Interfaces;
using ToDo.Infra.Base;
using ToDo.Infra.Repositories;

namespace ToDo.Api.DependencyInjections
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<ITodoRepository, TodoRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<IRefreshTokenCacheRepository, RefreshTokenCacheRepository>();
            services.AddScoped<IJwtAuthManager, JwtAuthManager>();
            services.AddScoped<ITodoService, ToDoService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            return services;
        }

        public static IServiceCollection ConfigureHealthCheck(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IHealthCheck, SelfHealthCheck>();

            services.AddHealthChecks()
             .AddDiskStorageHealthCheck(s => s.AddDrive("C:\\", 1024))
                .AddProcessAllocatedMemoryHealthCheck(512)
                .AddProcessHealthCheck("ProcessName", p => p.Length > 0)
                //.AddWindowsServiceHealthCheck("someservice", s => true)
                .AddUrlGroup(new Uri("https://localhost:44318/v1/HealthCheck/HealthCheck"), "HealthCheck ToDos")
                .AddSqlServer(configuration["ConnectionStrings"]);

            // services.AddHealthChecksUI(s =>
            // {
            //     s.AddHealthCheckEndpoint("HealthCheck ToDos", "https://localhost:44318/health");
            // })
            //    .AddInMemoryStorage();
            //
            // services.AddScoped<IHealthCheck, SelfHealthCheck>();

            return services;
        }

        public static IServiceCollection ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtTokenConfig = configuration.GetSection("jwtTokenConfig").Get<JwtTokenConfig>();
            services.AddSingleton(jwtTokenConfig);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = true;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtTokenConfig.Issuer,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtTokenConfig.Secret)),
                    ValidAudience = jwtTokenConfig.Audience,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(1)
                };
            });

            return services;
        }

        public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
        {
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

            return services;
        }

        public static IServiceCollection ConfigureMediatr(this IServiceCollection services)
        {
            var assembly = AppDomain.CurrentDomain.Load("ToDo.Domain");
            services.AddMediatR(assembly);
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationRequestBehavior<,>));
            services.AddScoped<IDomainNotificationContext, DomainNotificationContext>();

            services.AddTransient<IRequestHandler<CreateTodoCommand, GenericCommandResult>, CreateTodoCommandHandler>();
            services.AddTransient<IRequestHandler<GetAllToDoCommand, IEnumerable<TodoItem>>, GetAllToDoCommandHandler>();
            services.AddTransient<IRequestHandler<GetAllDoneToDoCommand, IEnumerable<TodoItem>>, GetAllDoneToDoCommandHandler>();
            services.AddTransient<IRequestHandler<GetAllUndoneToDoCommand, IEnumerable<TodoItem>>, GetAllUndoneToDoCommandHandler>();
            services.AddTransient<IRequestHandler<GetDoneForTodayToDoCommand, IEnumerable<TodoItem>>, GetDoneForTodayToDoCommandHandler>();
            services.AddTransient<IRequestHandler<GetUndoneForTodayToDoCommand, IEnumerable<TodoItem>>, GetUndoneForTodayToDoCommandHandler>();
            services.AddTransient<IRequestHandler<GetDoneForTomorrowToDoCommand, IEnumerable<TodoItem>>, GetDoneForTomorrowToDoCommandHandler>();
            services.AddTransient<IRequestHandler<GetUndoneForTomorrowToDoCommand, IEnumerable<TodoItem>>, GetUndoneForTomorrowToDoCommandHandler>();
            services.AddTransient<IRequestHandler<MarkTodoAsDoneCommand, GenericCommandResult>, MarkTodoAsDoneCommandHandler>();
            services.AddTransient<IRequestHandler<MarkTodoAsUndoneCommand, GenericCommandResult>, MarkTodoAsUndoneCommandHandler>();
            services.AddTransient<IRequestHandler<UpdateTodoCommand, GenericCommandResult>, UpdateTodoCommandHandler>();
            services.AddTransient<IRequestHandler<GetUserCommand, User>, GetUserCommandHandler>();
            services.AddTransient<IRequestHandler<GetRefreshTokenCommand, RefreshToken>, GetRefreshTokenCommandHandler>();
            services.AddTransient<IRequestHandler<UpdateRefreskTokenCommand, RefreshToken>, UpdateRefreshTokenCommandHandler>();
            services.AddTransient<IRequestHandler<AddRefreshTokenCommand, RefreshToken>, AddRefreshTokenCommandHandler>();

            

            return services;
        }
    }
}
