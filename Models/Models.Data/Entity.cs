// <copyright file="Entity.cs" company="Yarik Home">
// All rights maybe reserved. © Yarik
// </copyright>

namespace Models.Data
{
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// The base entity
    /// </summary>
    /// <typeparam name="T">type of Id.</typeparam>
    public class Entity<T>
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [Column("id")]
        public T Id { get; set; }
    }
}
