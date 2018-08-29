// <copyright file="ValidationError.cs" company="Yarik Home">
// All rights maybe reserved. © Yarik
// </copyright>

namespace Football.Web.Validation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// The validation error definition.
    /// </summary>
    public class ValidationError
    {
        /// <summary>
        /// Gets or sets the key. Name of invalid element.
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the error description.
        /// </summary>
        /// <value>
        /// The error.
        /// </value>
        public string Error { get; set; }
    }
}
