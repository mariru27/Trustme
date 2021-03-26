using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Trustme.IServices;
using Trustme.Service;
using AppContext = Trustme.Data.AppContext;
using Trustme.ITools;
using Trustme.Tools;

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

            //services.AddScoped<AppContext>();
            services.AddEntityFrameworkSqlServer().AddDbContext<AppContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")).EnableSensitiveDataLogging());

           //services.AddScoped<AppContext>();



            services.AddHttpContextAccessor();
            services.AddScoped<IKeyRepository, KeyRepository>();
            services.AddScoped<IUnsignedDocumentRepository, UnsignedDocumentRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IHttpRequestFunctions, HttpRequestFunctions>();
            services.AddTransient<ICertificate, Certificate>();
            services.AddTransient<ISignedDocumentRepository, SignedDocumentRepository>();
            services.AddTransient<ISign, Sign>();
            services.AddSingleton<ICrypto, Crypto>();

            services.AddMvc().AddControllersAsServices();

            services.AddAuthentication("CookieAuth").AddCookie("CookieAuth", config =>
            {
                config.Cookie.Name = "User.cookie";
                config.LoginPath = "/Authenticate/LogIn";
                config.LogoutPath = "/Authenticate/LogOut";
            });
            services.AddControllersWithViews();
            services.AddMemoryCache();



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
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
