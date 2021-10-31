using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Whatso.App.Helper;
using Whatso.Configuartion;

namespace Whatso.App
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
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
               .AddCookie(options =>
               {
                   options.LoginPath = "/Login/Index";

               });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //SessionManager._httpContextAccessor = services.BuildServiceProvider().GetService<IHttpContextAccessor>();
            services.AddSingleton<IAppConfiguration, AppConfiguration>();
            services.AddSingleton<IAPIHelper, APIHelper>();
            //services.AddScoped<IAccountService, AccountService>();
            //services.AddScoped<IAccountRepository, AccountRepository>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddMvc(option => option.EnableEndpointRouting = false);
            services.AddRouting();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseAuthentication();
            //app.UseMvcWithDefaultRoute();
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
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "default_route",
                template: "{controller}/{action}/{id?}",
                defaults: new { controller = "Home", action = "Index" });
                //routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
