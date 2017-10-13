using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Querier.Models.Login;
using Querier.Models;

namespace Querier
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Added Entity Framework 
            //NOTE: Here is where we need to add the address of the Heroku Database.
            services.AddDbContext<LoginDatabase>(options => options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Login"));

            //Added Identity Framework
            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<LoginDatabase>();

            //Added MVC Framework
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            //This allows authentication of users.
            app.UseAuthentication();

            //This allows linking to css, jquery, etc.
            app.UseStaticFiles();

            //This is where you want the app to route you to when you load the app.
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
