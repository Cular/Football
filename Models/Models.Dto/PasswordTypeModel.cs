// <copyright file="PasswordTypeModel.cs" company="Yarik Home">
// All rights maybe reserved. © Yarik
// </copyright>

namespace Models.Dto
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    /// <summary>
    /// OAuth authorization model.
    /// </summary>
    public class PasswordTypeModel
    {
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        [MinLength(4, ErrorMessage = "Minimal length equals 4.")]
        [MaxLength(12, ErrorMessage = "Maximal lenght equals 12.")]
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        [MinLength(5, ErrorMessage = "Minimal length equals 5.")]
        [MaxLength(12, ErrorMessage = "Maximal lenght equals 12.")]
        public string Password { get; set; }
    }
}
