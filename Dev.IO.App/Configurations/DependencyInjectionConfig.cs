using Dev.IO.Data.Context;
using Dev.IO.Data.Repository;
using DevIO.Bussiness.Interfaces;
using DevIO.Bussiness.Notifications;
using DevIO.Bussiness.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dev.IO.App.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<MyDbContext>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ISupplierRepository, SupplierRepository>();
            services.AddScoped<IAndressRepository, AndressRepository>();

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ISupplierService, SupplierService>();
            services.AddScoped<INotify, Notify>();

            return services;
        }
        
    }
}
