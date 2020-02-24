using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MarsRover.API.Data;
using MarsRover.API.Data.Interfaces;
using MarsRover.API.Data.Repositories;
using MarsRover.API.Library.Interfaces;
using MarsRover.API.Library.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MarsRover.API
{
    public class Startup
    {
        IServiceCollection services;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            this.services = services;
            this.services.Configure<IISServerOptions>(options =>
           {
               options.AllowSynchronousIO = true;
           });
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddMemoryCache();
            services.AddDbContext<DataContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("SqlConnection")));
            services.AddCors();
            services.AddAutoMapper(typeof(RoverRepository).Assembly);


            services.AddControllers()
.AddNewtonsoftJson(opt =>
{
    opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

});

            services.AddScoped<IRoverRepository, RoverRepository>();
            services.AddScoped<IRoverMovementRepository, RoverMovementRepository>();
            services.AddScoped<IMarsGridRepository, MarsGridRepository>();
            //services
            services.AddScoped<IValidationDictionary, Validation>();
            services.AddScoped<IRoverService, RoverService>();
            services.AddScoped<IRoverMovementService, RoverMovementService>();
            services.AddScoped<IMarsGridService, MarsGridService>();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());


            app.UseEndpoints(endPoints =>
            {
                endPoints.MapControllers();
                // endPoints.MapFallbackToController("Index", "Fallback");
            });

        }

        public class ConfigurationService
        {
            public IConfiguration Configuration { get; private set; }

            public IWebHostEnvironment Environment { get; private set; }
            public ConfigurationService(IWebHostEnvironment environment)
            {
                this.Environment = environment;

                var configFileName = System.IO.Path.Combine(environment.ContentRootPath, "appsettings.json");
                var config = new ConfigurationBuilder()
                                .AddJsonFile(configFileName, true)
                                .Build();
                this.Configuration = config;
            }
        }
    }
}
