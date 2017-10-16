using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Lucykids_v2.Data;
using Lucykids_v2.Models;
using Lucykids_v2.Services;
using Lucykids_v2.DAL;
using Lucykids_v2.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Lucykids_v2
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            //if (env.IsDevelopment())
            //{
            //    // For more details on using the user secret store see https://go.microsoft.com/fwlink/?LinkID=532709
            //    builder.AddUserSecrets<Startup>();
            //}

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            // Add framework services.
            services.AddDbContext<StoreDbContext>(options =>
                options.UseSqlServer(Configuration["Data:StoreProducts:ConnectionString"]));

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration["Data:StoreIdentity:ConnectionString"]));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            
            #region
            // Configure Identity
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 9;

                // Cookie settings
                options.Cookies.ApplicationCookie.ExpireTimeSpan = TimeSpan.FromDays(150);
                options.Cookies.ApplicationCookie.LoginPath = "/Account/LogIn";
                options.Cookies.ApplicationCookie.LogoutPath = "/Account/LogOut";

                // User settings
                options.User.RequireUniqueEmail = true;
            });
            #endregion

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IBrandRepository, BrandRepository>();
            services.AddTransient<ISizeRepository, SizeRepository>();

            // Sign in manager
            services.AddScoped<SignInManager<IdentityUser>, SignInManager<IdentityUser>>();   
                services.AddScoped<SignInManager<ApplicationUser>, SignInManager<ApplicationUser>>();



            // allows me to work with the context database
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // addScoped creates an object that is associated with a request, but this differ to different requests
            // so when I browse the site I'll get different instance of shopping cart from other user
            services.AddScoped<Cart>(crt => Cart.GetCart(crt));

            //  session services
            services.AddMemoryCache();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            // Identity is enabled for the application by calling UseIdentity. 
            // Use Identity adds cookie-based authetication middleware to the request pipeline
            app.UseIdentity();
            app.UseSession();

            // Add external authentication middleware below. To configure them please see https://go.microsoft.com/fwlink/?LinkID=532715

            app.UseMvc(routes =>
            {
                //routes.MapRoute(
                //    name: "default",
                //    template: "{controller=Products}/{action=Index}/{id?}");
                //routes.MapRoute(
                //    name:"ProductsIndex",
                //    template:"Products",
                //    defaults: "{controller =Products}/{action=Index}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            SeedData.EnsurePopulated(app);
            //IdentitySeedData.EnsurePopulated(app);
        }
    }
}
