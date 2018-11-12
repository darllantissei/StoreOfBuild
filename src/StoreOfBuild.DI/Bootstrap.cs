using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StoreOfBuild.Data;
using StoreOfBuild.Data.Contexts;
using StoreOfBuild.Data.Repositores;
using StoreOfBuild.Domain;
using StoreOfBuild.Domain.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreOfBuild.DI
{
    public static class Bootstrap
    {
        public static void Configure(IServiceCollection services, string connection)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connection));

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddScoped(typeof(IRepository<Product>), typeof(ProductRepository));

            services.AddScoped(typeof(CategoryStorer));

            services.AddScoped(typeof(ProductStorer));

            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
        }


    }
}
