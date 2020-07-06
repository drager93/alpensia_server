using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using alpensia_server.Databases;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace alpensia_server
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
            // DB 셋팅
            var connectionString = string.IsNullOrEmpty(Configuration["AlpensiaDatabase"]) ?
            Configuration.GetConnectionString("AlpensiaDatabase") : Configuration["AlpensiaDatabase"];

            services.AddDbContext<DBContext>(options =>
                    options.UseLazyLoadingProxies().UseMySql(connectionString));

            // CORS
            //Cors 셋팅
            services.AddCors(options =>
            {
                var origins = Configuration.GetSection("AllowedOrigins").GetChildren().Select(a => a.Value).ToArray();
                var builder = new CorsPolicyBuilder(origins);
                builder.AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
                options.AddDefaultPolicy(builder.Build());
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseCors();

            //app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}