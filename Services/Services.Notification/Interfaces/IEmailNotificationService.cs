// <copyright file="IEmailNotificationService.cs" company="Yarik Home">
// All rights maybe reserved. © Yarik
// </copyright>

namespace Services.Notification.Interfaces
{
    using System.Threading.Tasks;
    using Models.Notification;

    /// <summary>
    /// Email implementation of notification.
    /// </summary>
    public interface IEmailNotificationService
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
