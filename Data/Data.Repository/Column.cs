// <copyright file="Column.cs" company="Yarik Home">
// All rights maybe reserved. © Yarik
// </copyright>

namespace Data.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// The column.
    /// </summary>
    public class Column
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the property.
        /// </summary>
        /// <value>
        /// The property.
        /// </value>
        public string Property { get; set; }

        /// <summary>
        /// Gets or sets the setter.
        /// </summary>
        /// <value>
        /// The setter.
        /// </value>
        public Action<object, object> Setter { get; set; }

        /// <summary>
        /// Gets or sets the getter.
        /// </summary>
        /// <value>
        /// The getter.
        /// </value>
        public Func<object, object> Getter { get; set; }
    }
}
