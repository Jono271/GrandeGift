using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
//....................
using TAFE2018GrandeGiftFinal.Services;
using TAFE2018GrandeGiftFinal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace TAFE2018GrandeGiftFinal
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddScoped<IDataService<Customer>, DataService<Customer>>();
            services.AddScoped<IDataService<Category>, DataService<Category>>();
            services.AddScoped<IDataService<Product>, DataService<Product>>();
            services.AddScoped<IDataService<OrderItem>, DataService<OrderItem>>();
            services.AddScoped<IDataService<Address>, DataService<Address>>();
            services.AddScoped<IDataService<Order>, DataService<Order>>();
            services.AddIdentity<IdentityUser, IdentityRole>
            (
                config =>
                {
                    config.Password.RequireDigit = true;
                    config.Password.RequiredLength = 6;
                    config.Password.RequireUppercase = true;
                    config.Password.RequireLowercase = true;    
                    config.User.RequireUniqueEmail = true;
                }
            ).AddEntityFrameworkStores<MyDbContext>();
            services.AddDbContext<MyDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddMvc().AddSessionStateTempDataProvider();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStaticFiles();
            app.UseSession();
            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();

            //seed
            SeedHelper.Seed(app.ApplicationServices).Wait();
        }
    }
}
