using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EGeladinho.Src.Contexts;
using EGeladinho.Src.Models;
using EGeladinho.Src.Repository;
using EGeladinho.Src.Repository.Implements;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EGeladinho
{
    public class Startup
    {
        public IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Define string connection
            services.AddDbContext<GeladinhoDBC>(opt => opt.UseSqlServer(_configuration["ConnectionStringsDev:GeladinhoDB"]));
        
            // Add scope Repository
            services.AddScoped<ICrud<User>, UserRepository>();
            services.AddScoped<ICrud<Product>, ProductRepository>();
            services.AddScoped<ICrud<Cart>, CartRepository>();
        
            // Add scope Controllers
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, GeladinhoDBC context)
        {
            // Enviroment Development
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                context.Database.EnsureCreated();
            }

            // Enviroment Production
            context.Database.EnsureCreated();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
