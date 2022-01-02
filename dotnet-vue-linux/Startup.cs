using ElectronNET.API;
using ElectronNET.API.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Proggmatic.SpaServices.VueCli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_vue_linux
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "dotnet_vue_linux", Version = "v1" });
            });

            //Ìí¼ÓÇ°¶ËÄ¿Â¼
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "dotnet-vue/dist";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseSwagger();
                //app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "dotnet_vue_linux v1"));
            }

            app.UseStaticFiles();

            app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                //endpoints.MapControllerRoute(name: default, pattern: "{controller=Weather}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "dotnet-vue";        
                spa.Options.PackageManagerCommand = "yarn";
                if (env.IsDevelopment())
                {
                    spa.UseVueCliServer();

                    // Or to build not by starting this application but manually uncomment next lines and comment line above
                    //spa.ApplicationBuilder.UseFixSpaPathBaseBugMiddleware();
                    // spa.UseProxyToSpaDevelopmentServer("http://localhost:8080");
                }
            });

            BootStrapConfig();
        }

        public async void BootStrapConfig()
        {
            await Task.Run(async () => await Electron.WindowManager.CreateWindowAsync());
        }
    }
}
