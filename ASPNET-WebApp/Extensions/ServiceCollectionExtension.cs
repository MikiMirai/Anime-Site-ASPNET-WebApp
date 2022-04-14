﻿using ASPNET_WebApp.Core.Contracts;
using ASPNET_WebApp.Core.Services;
using ASPNET_WebApp.Infrastructure.Data;
using ASPNET_WebApp.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ASPNET_WebApp.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IApplicationDbRepository, ApplicationDbRepository>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }

        public static IServiceCollection AddApplicationDbContexts(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            services.AddDatabaseDeveloperPageExceptionFilter();

            return services;
        }
    }
}
