// <copyright file="Startup.cs" company="Yarik Home">
// All rights maybe reserved. © Yarik
// </copyright>

namespace Football.Web
{
    using System;
    using System.IO;
    using AutoMapper;
    using Data.DataBaseContext;
    using Data.Repository.Implementation;
    using Data.Repository.Interfaces;
    using Football.Core.Extensions;
    using Football.Core.Middleware;
    using Football.Web.Validation;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Models.Mapper;
    using Models.Notification;
    using Swashbuckle.AspNetCore.SwaggerGen;

    /// <summary>
    /// The startup.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public Startup(IConfiguration configuration)
        {
            this.Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddConfiguration(configuration)
                .Build();
        }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">The services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(ValidationModelAttribute));
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddAutoMapper(typeof(MainProfile));

            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info { Version = "v1", Title = "Football API" });
                IncludeComments(x);
            });

            services.AddDbContext<FootballContext>(
                opt =>
                {
                    opt.UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection"));
                },
                contextLifetime: ServiceLifetime.Scoped,
                optionsLifetime: ServiceLifetime.Scoped);

            services.AddSmtpClient();

            services.AddScoped<IPlayerRepository, PlayerRepository>();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="env">The env.</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Football API V1");
            });

            app.UseMvc();
        }

        /// <summary>
        /// Includes the comments.
        /// </summary>
        /// <param name="options">The swagger options.</param>
        private static void IncludeComments(SwaggerGenOptions options)
        {
            var modelsDoc = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Models.Dto.xml");
            if (File.Exists(modelsDoc))
            {
                options.IncludeXmlComments(modelsDoc);
            }

            var apiDoc = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Football.Web.xml");
            if (File.Exists(apiDoc))
            {
                options.IncludeXmlComments(apiDoc);
            }
        }
    }
}
