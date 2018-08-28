// <copyright file="Program.cs" company="Yarik Home">
// All rights maybe reserved. © Yarik
// </copyright>

namespace Football.Web
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// The main class.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();
            host.Run();
        }

        /// <summary>
        /// Creates the web host builder.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns>The host.</returns>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
