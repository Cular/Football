// <copyright file="BatchMessage.cs" company="Yarik Home">
// All rights maybe reserved. © Yarik
// </copyright>

namespace Models.Notification
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// The message for list of users.
    /// </summary>
    public class BatchMessage
    {
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the users identifiers.
        /// </summary>
        /// <value>
        /// The users identifiers.
        /// </value>
        public List<string> UsersIds { get; set; }
    }
}
