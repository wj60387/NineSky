using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Ninesky.Models;
using Newtonsoft.Json.Linq;
using System.IO;
using Newtonsoft.Json;
using System.Reflection;
using System.Runtime.Loader;
using Microsoft.Extensions.DependencyModel;

namespace Ninesky.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            if (env.IsDevelopment())
            {
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);
            services.AddDbContext<NineskyDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<DbContext, NineskyDbContext>();
            //services.AddScoped<CategoryService>();
            services.AddMvc();
            var JsonAssembly = JObject.Parse(File.ReadAllText("service.json"))["Assembly"];
            var requiredServices = JsonConvert.DeserializeObject<List<AssemblyItem>>(JsonAssembly.ToString());
            Assembly interfaceAssembly = Assembly.Load(new AssemblyName("Ninesky.InterfaceBase"));
            var k2 = AssemblyLoadContext.Default.LoadFromAssemblyPath(AppContext.BaseDirectory + "//Ninesky.Base.dll");
            foreach (var assembly in JsonAssembly.Values())
            {
                var jsonServices = assembly["DIService"];
                //var requiredServices = JsonConvert.DeserializeObject<List<ServiceItem>>(jsonServices.ToString());
                foreach (var service in requiredServices)
                {
                    //var aa = new ServiceDescriptor(serviceType: interfaceAssembly.GetType(service.serviceType), implementationType: k2.GetType(service.implementationType), lifetime: service.lifetime);
                    //services.Add(aa);
                }
                var k1 = 0;
            }



             var aa = Microsoft.Extensions.DependencyInjection.ServiceLifetime.Scoped;


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseApplicationInsightsRequestTelemetry();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseApplicationInsightsExceptionTelemetry();

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "area",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

            });
        }


    }
}
