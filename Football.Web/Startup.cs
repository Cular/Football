// <copyright file="Startup.cs" company="Yarik Home">
// All rights maybe reserved. © Yarik
// </copyright>

namespace Football.Web
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using AutoMapper;
    using Data.DataBaseContext;
    using Data.Repository.Implementation;
    using Data.Repository.Interfaces;
    using Football.Chat.Models.Mapper;
    using Football.Chat.Repository.Extensions;
    using Football.Core.Extensions;
    using Football.Core.Middleware;
    using Football.Web.Hubs;
    using Football.Web.Validation;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Models.Infrastructure;
    using Models.Mapper;
    using Services.Game;
    using Services.Identity;
    using Services.Notification;
    using Services.Players;
    using Services.Registration;
    using Swashbuckle.AspNetCore.Swagger;
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
                .AddEnvironmentVariables()
                .Build();

            this.TokenConfiguration = this.Configuration.GetSection("TokenConfiguration").Get<TokenConfiguration>();
        }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Gets the token configuration.
        /// </summary>
        /// <value>
        /// The token configuration.
        /// </value>
        private TokenConfiguration TokenConfiguration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">The services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MainProfile), typeof(ChatProfile));

            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new Info { Version = "v1", Title = "Football API" });

                var oauthScheme = new OAuth2Scheme
                {
                    Description =
                        "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    TokenUrl = "/token",
                    Flow = "password"
                };

                x.AddSecurityDefinition("oauth2", oauthScheme);
                x.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>> { { "oauth2", new string[] { } } });

                IncludeComments(x);
            });

            services.AddDbContext<FootballContext>(
                opt =>
                {
                    opt.UseLazyLoadingProxies();
                    opt.UseNpgsql(this.Configuration.GetConnectionString("SqlConnection"));

                    //opt.UseSqlServer(this.Configuration.GetConnectionString("SqlConnection"));
                },
                contextLifetime: ServiceLifetime.Scoped,
                optionsLifetime: ServiceLifetime.Scoped);

            // Data
            services.AddScoped<IPlayerRepository, PlayerRepository>();
            services.AddScoped<IPlayerActivationRepository, PlayerActivationRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<IGameRepository, GameRepository>();
            services.AddScoped<IMeetingTimeRepository, MeetingTimeRepository>();
            services.AddScoped<IFriendshipRepository, FriendshipRepository>();
            services.AddChatRepository();

            // Services
            services.AddHttpClient();
            services.AddTokenConfiguration(this.TokenConfiguration);
            services.AddSmtpClient();
            services.AddEmailService();
            services.AddFirebaseService();
            services.AddScoped<IRegisterNotifier, RegisterNotifier>();
            services.AddSingleton(this.TokenConfiguration);
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IGameService, GameService>();
            services.AddScoped<IPlayerService, PlayerService>();
            services.AddScoped<IRegistrationService, RegistrationService>();

            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(ValidationModelAttribute));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddCors();

            services.AddSignalR()
                .AddHubOptions<ChatHub>(options => options.EnableDetailedErrors = true);
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
            var wsOptions = new WebSocketOptions();

#if DEBUG
            app.UseCors(builder =>
            {
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
                builder.WithOrigins("http://localhost:5010", "http://localhost");
                builder.AllowCredentials();
            });

            wsOptions.AllowedOrigins.Add("http://localhost:5010");
            wsOptions.AllowedOrigins.Add("http://localhost");
#endif
            app.UseWebSockets(wsOptions);
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Football API V1");
            });

            app.UseSignalR(hrb => hrb.MapHub<ChatHub>("/hubs/chat"));
            app.UseAuthentication();
            app.UseHttpsRedirection();
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
