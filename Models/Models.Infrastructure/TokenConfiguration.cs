// <copyright file="TokenConfiguration.cs" company="Yarik Home">
// All rights maybe reserved. © Yarik
// </copyright>

namespace Models.Infrastructure
{
    using System;

    /// <summary>
    /// The token configuration.
    /// </summary>
    public class TokenConfiguration
    {
        /// <summary>
        /// Gets or sets the path.
        /// </summary>
        /// <value>
        /// The path.
        /// </value>
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the issuer.
        /// </summary>
        /// <value>
        /// The issuer.
        /// </value>
        public string Issuer { get; set; }

        /// <summary>
        /// Gets or sets the audience.
        /// </summary>
        /// <value>
        /// The audience.
        /// </value>
        public string Audience { get; set; }

        /// <summary>
        /// Gets or sets the expiration.
        /// </summary>
        /// <value>
        /// The expiration.
        /// </value>
        public TimeSpan Expiration { get; set; }

        /// <summary>
        /// Gets or sets the refresh expiration.
        /// </summary>
        /// <value>
        /// The refresh expiration.
        /// </value>
        public TimeSpan RefreshExpiration { get; set; }
    }
}
