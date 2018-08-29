// <copyright file="PlayerDtoCreate.cs" company="Yarik Home">
// All rights maybe reserved. © Yarik
// </copyright>

namespace Models.Dto
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// The create player dto model.
    /// </summary>
    public class PlayerDtoCreate
    {
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        [MinLength(5, ErrorMessage = "Minimal length equals 5.")]
        [MaxLength(12, ErrorMessage = "Maximal lenght equals 12.")]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the alias.
        /// </summary>
        /// <value>
        /// The alias.
        /// </value>
        [MinLength(4, ErrorMessage = "Minimal length equals 4.")]
        [MaxLength(12, ErrorMessage = "Maximal lenght equals 12.")]
        public string Alias { get; set; }
    }
}
