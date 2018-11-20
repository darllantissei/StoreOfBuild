using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StoreOfBuild.Data;
using StoreOfBuild.Data.Contexts;
using StoreOfBuild.Data.Identity;
using StoreOfBuild.Data.Repositores;
using StoreOfBuild.Domain;
using StoreOfBuild.Domain.Account;
using StoreOfBuild.Domain.Products;
using StoreOfBuild.Domain.Sales;
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

            services.AddIdentity<ApplicationUser, IdentityRole>(config =>
            {
                config.Password.RequireDigit = false;
                config.Password.RequiredLength = 3;
                config.Password.RequireLowercase = false;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireUppercase = false;
                /*config.Cookies.ApplicationCookie.LoginPath = "/Account/Login";*/
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddScoped(typeof(IAuthentication), typeof(Authentication));

            services.AddScoped(typeof(IRepository<Product>), typeof(ProductRepository));

            services.AddScoped(typeof(CategoryStorer));

            services.AddScoped(typeof(ProductStorer));

            services.AddScoped(typeof(SaleFactory));

            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));            
        }


    }
}
