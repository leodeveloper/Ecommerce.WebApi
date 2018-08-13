
using Ecommerce.Model.Dto;
using Ecommerce.Model.EntityFrameWork;
using Ecommerce.Model.GenericRepository.Implementation;
using Ecommerce.Model.GenericRepository.Repository;
using Ecommerce.Service.Interface;
using Ecommerce.Service.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSwag.AspNetCore;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Ecommerce.WebApi
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddDbContext<EnityFramWorkDbContext>(opt => opt.UseInMemoryDatabase("TestDb"));
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<IRepository, EntityFrameworkRepository>();
            services.AddScoped<IRepositoryReadOnly, EntityFrameworkRepositoryReadOnly>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IServiceProvider serviceProvider)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            if (env.IsDevelopment())
            {
                var context = serviceProvider.GetService<EnityFramWorkDbContext>();
                AddTestData(context);
            }
            else
            {
                throw new NotImplementedException("Actual Database context not implemented");
            }
            app.UseSwaggerUi(typeof(Startup).GetTypeInfo().Assembly, null);
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Product}/{action=Index}/{id?}");
            });
        }

        private static void AddTestData(EnityFramWorkDbContext context)
        {
            IList<Product> products = new List<Product>();
            products.Add(new Product { Id = 1, Name = "Water Glass", Price = 10.00M, Photo = "", Desciption = "Water Glass for drinking" });
            products.Add(new Product { Id = 2, Name = "Jug Water Glass", Price = 12.00M, Photo = "", Desciption = "Water Glass for drinking" } );
            products.Add(new Product { Id = 3, Name = "Green Water Glass", Price = 13.00M, Photo = "", Desciption = "Water Glass for drinking" });
            products.Add(new Product { Id = 4, Name = "Red Water Glass", Price = 14.00M, Photo = "", Desciption = "Water Glass for drinking" });
            products.Add(new Product { Id = 5, Name = "Yellow Water Glass", Price = 15.00M, Photo = "", Desciption = "Water Glass for drinking" });
            context.Products.AddRange(products);

            IList<BasketItem> basketItems = new List<BasketItem>();
            basketItems.Add(new BasketItem { Id = 1, ProductId = 1, Quantity = 2, UserId = 1 });
            basketItems.Add(new BasketItem { Id = 2, ProductId = 5, Quantity = 1, UserId = 1 });
            context.BasketItems.AddRange(basketItems);

            context.SaveChanges();
        }
    }
}
