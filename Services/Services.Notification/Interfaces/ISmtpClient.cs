// <copyright file="ISmtpClient.cs" company="Yarik Home">
// All rights maybe reserved. © Yarik
// </copyright>

namespace Services.Notification.Interfaces
{
    using System;
    using System.Net.Mail;
    using System.Threading.Tasks;

    /// <summary>
    /// Wrap on SmtpClient.
    /// </summary>
    public interface ISmtpClient : IDisposable
    {
        /// <summary>
        /// Sends the mail asynchronous.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>Void result.</returns>
        Task SendMailAsync(MailMessage message);
    }
}
