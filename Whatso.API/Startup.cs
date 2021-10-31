using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using Whatso.API.JSON.Serializers;
using Whatso.Configuartion;
using Whatso.Repository;
using Whatso.Repository.Interface;
using Whatso.Services;
using Whatso.Services.Interface;

namespace Whatso.API
{
    public class Startup
    {
        public Startup(Microsoft.AspNetCore.Hosting.IHostingEnvironment env, IConfiguration configuration)
        {
            Configuration = configuration;
            var contentRoot = env.ContentRootPath;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            /// Response Compression
            services.AddResponseCompression();
            services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Optimal;
            });
            services.AddMvc(option => option.EnableEndpointRouting = false);

            services.AddSingleton<IAppConfiguration, AppConfiguration>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IJsonFieldsSerializer, JsonFieldsSerializer>();

            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IAccountRepository, AccountRepository>();

            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IOrderRepository, OrderRepository>();

            services.AddMemoryCache();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Frigate API",
                    Version = "v1",
                    Description = "Frigate API"
                });
            });
            services.AddMvcCore()
                    .AddApiExplorer();
            services.AddMvc(options =>
            {
                //options.Filters.Add(typeof(UserIdentityFilter));

            });

            //services.AddScoped<UserAuthorization>();
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                // VAPT : Http overrides the forwarded headers
                options.ForwardedHeaders =
                    Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.All;
            });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader()
                .Build());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/v1/swagger.json", "Whatso API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                      Path.Combine(Directory.GetCurrentDirectory(), "Documents")),
                RequestPath = "/Documents"
            });
        }
    }
}
