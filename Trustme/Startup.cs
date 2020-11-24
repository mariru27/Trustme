using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Trustme.Controllers;
using Trustme.Data;
using AppContext = Trustme.Data.AppContext;

namespace Trustme
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

            //services.AddSingleton<Administration>();

            services.AddMvc().AddControllersAsServices();
            services.AddHttpContextAccessor();
            //services.AddSingleton<Administration>();
            //services.AddMvc();
            //services.AddScoped<DbContext, AppContext>();
            //services.AddScoped<Administration>();
            //services.AddScoped<SignDocuments>();

            //services.AddSingleton<DbContextOptions>();
            //services.AddSingleton<AppContext>();
            //services.AddSingleton<Administration>();
            //services.AddTransient<SignDocuments>();

            services.AddAuthentication("CookieAuth").AddCookie("CookieAuth", config =>
            {
                config.Cookie.Name = "User.cookie";
                config.LoginPath = "/Administration/LogIn";
                config.LogoutPath = "/Administration/LogOut";
            });
            services.AddEntityFrameworkSqlServer().AddDbContext<AppContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddControllersWithViews();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            //app.UseCookiePolicy();
            //app.UseAuthentication();
            ////app.UseSession();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Administration}/{action=Index}/{id?}");
            });
        }
    }
}
