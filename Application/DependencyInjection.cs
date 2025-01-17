﻿using Application.Interface;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IDapperPostService, DapperPostService>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<ICosmosPostService, CosmosPostService>();
            return services;
        }
    }
}
