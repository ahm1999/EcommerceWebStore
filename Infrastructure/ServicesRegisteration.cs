﻿using Application.ServiceInterfaces;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Infrastructure
{
    public static class ServicesRegisteration
    {

        public static void RegisterEntitiesServices(this IServiceCollection services) {
            services.AddScoped<IProductService, ProductService>();
            
        }

        public static void DbConnection(this IServiceCollection services, IConfiguration configuration)
        {
            string DbConnectionString  = configuration.GetConnectionString("SqlServer")!;
            services.AddDbContext<AppDbContext>(option => option.UseSqlServer(DbConnectionString));

        }

    }
}
