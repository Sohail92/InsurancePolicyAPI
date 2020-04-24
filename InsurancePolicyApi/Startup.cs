using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InsurancePolicyApi.Interfaces;
using InsurancePolicyApi.Logic;
using InsurancePolicyApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace InsurancePolicyApi
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
            services.AddControllers().AddJsonOptions((options => options.JsonSerializerOptions.WriteIndented = true));
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            // Here is where we setup our Dependency Injection we can use Scoped, Transient or Singleton as required.

            // Scoped lifetime services are created once per request within the scope. It is equivalent to a singleton in the current scope. For example, in MVC it creates one instance for each HTTP request, but it uses the same instance in the other calls within the same web request.
            services.AddScoped<IRetrievePolicies, PolicyRetrievalLogic>();
            services.AddScoped<IStorePolicies, PolicyStoringLogic>();

            //Transient lifetime services are created each time they are requested. This lifetime works best for lightweight, stateless services.
            //services.AddTransient<IRetrievePolicies, PolicyRepository>();

            //Singleton which creates a single instance throughout the application.It creates the instance for the first time and reuses the same object in the all calls.
            //services.AddSingleton<IRetrievePolicies, PolicyRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
