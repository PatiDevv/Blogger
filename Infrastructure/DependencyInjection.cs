﻿using Domain.Interface;
using Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IDapperPostRepository, DapperPostRepository>();
            services.AddScoped<ICosmosPostRepository, CosmosPostRepository>();
            return services;
        }
    }
}
