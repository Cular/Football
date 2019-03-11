// <copyright file="Schema.cs" company="Yarik Home">
// All rights maybe reserved. © Yarik
// </copyright>

namespace Data.Repository
{
    using System.Collections.Generic;

    /// <summary>
    /// The schema.
    /// </summary>
    public class Schema
    {
        /// <summary>
        /// Gets or sets the name of the schema.
        /// </summary>
        /// <value>
        /// The name of the schema.
        /// </value>
        public string SchemaName { get; set; }

        /// <summary>
        /// Gets or sets the name of the table.
        /// </summary>
        /// <value>
        /// The name of the table.
        /// </value>
        public string TableName { get; set; }

        /// <summary>
        /// Gets or sets the name of the key.
        /// </summary>
        /// <value>
        /// The name of the key.
        /// </value>
        public string KeyName { get; set; }

        /// <summary>
        /// Gets or sets the columns.
        /// </summary>
        /// <value>
        /// The columns.
        /// </value>
        public List<Column> Columns { get; set; }
    }
}
