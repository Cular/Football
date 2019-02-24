// <copyright file="IPushNotificationService.cs" company="Yarik Home">
// All rights maybe reserved. © Yarik
// </copyright>

namespace Services.Notification.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using Models.Notification;

    /// <summary>
    /// The push implementation of client notification.
    /// </summary>
    public interface IPushNotificationService
    {
        /// <summary>
        /// Notifies this instance.
        /// </summary>
        /// <param name="message">The message for user.</param>
        /// <returns>Void result.</returns>
        Task Notify(Message message);

        /// <summary>
        /// Notifies the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>Void result.</returns>
        Task Notify(BatchMessage message);
    }
}
