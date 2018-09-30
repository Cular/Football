// <copyright file="EmailService.cs" company="Yarik Home">
// All rights maybe reserved. © Yarik
// </copyright>

namespace Services.Notification
{
    using System;
    using System.Collections.Generic;
    using System.Net.Mail;
    using System.Text;
    using System.Threading.Tasks;
    using Models.Notification;
    using Services.Notification.Intefraces;

    /// <summary>
    /// Handle email sending to user.
    /// </summary>
    /// <seealso cref="Services.Notification.INotificationService" />
    public class EmailService : INotificationService
    {
        private readonly SmtpClient smtpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailService"/> class.
        /// </summary>
        /// <param name="smtpClient">The smptClient</param>
        public EmailService(SmtpClient smtpClient)
        {
            this.smtpClient = smtpClient ?? throw new ArgumentNullException(nameof(smtpClient));
        }

        /// <summary>
        /// Notifies this instance.
        /// </summary>
        /// <param name="message">The message for user.</param>
        /// <returns>
        /// Void result.
        /// </returns>
        public Task Notify(Message message)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("cular.live@gmail.com");
            mail.To.Add(new MailAddress(message.UserId));
            mail.Subject = message.Title;
            mail.Body = message.Text;

            return this.smtpClient.SendMailAsync(mail);
        }
    }
}
