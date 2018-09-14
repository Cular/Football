// <copyright file="INotificationService.cs" company="Yarik Home">
// All rights maybe reserved. © Yarik
// </copyright>

namespace Services.Notification
{
    using System;
    using System.Threading.Tasks;
    using Models.Notification;

    /// <summary>
    /// Notification service.
    /// </summary>
    public interface INotificationService
    {
        /// <summary>
        /// Notifies this instance.
        /// </summary>
        /// <param name="message">The message for user.</param>
        /// <returns>Void result.</returns>
        Task Notify(Message message);
    }
}
