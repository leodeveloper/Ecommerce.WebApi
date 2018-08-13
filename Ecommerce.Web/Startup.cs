using AutoMapper;
using Ecommerce.Model.Dto;
using Ecommerce.Web.Model;
using Ecommerce.Web.Repositories;
using Ecommerce.WebApi.Client.Interface;
using Ecommerce.WebApi.Client.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Ecommerce.Web
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
            //Automapper profile
            services.AddAutoMapper();
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Product, ProductViewModel>();               
                cfg.CreateMap<IList<Product>, IList<ProductViewModel>>();
                cfg.CreateMap<BasketItem, BasketItemViewModel>();
                cfg.CreateMap<IList<BasketItem>, IList<BasketItemViewModel>>();
            });
            var mapper = config.CreateMapper();

            services.AddScoped<IGetRequestManager, GetRequestManager>();
            services.AddScoped<IPostRequestManager, PostRequestManager>();
            services.AddScoped<IPutRequestManager, PutRequestManager>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<IDeleteRequestManager, DeleteRequestManager>();
           

            services.AddScoped<IGetApiUrls, GetApiUrls>();
            services.AddMvc();
            // Add functionality to inject IOptions<T>
            services.AddOptions();

            services.Configure<ApiUrls>(Configuration.GetSection("ApiUrls"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseStatusCodePagesWithReExecute("../Error/Index","?StatusCode={0}");
            }
            
            app.UseStaticFiles();            
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        
    }
}
