// <copyright file="GoogleSmtpClient.cs" company="Yarik Home">
// All rights maybe reserved. © Yarik
// </copyright>

namespace Services.Notification
{
    using System;
    using System.Net;
    using System.Net.Mail;
    using System.Threading.Tasks;
    using Services.Notification.Intefraces;

    /// <summary>
    /// The google smtp provider.
    /// </summary>
    /// <seealso cref="Services.Notification.Intefraces.ISmtpClient" />
    /// <seealso cref="System.IDisposable" />
    public class GoogleSmtpClient : ISmtpClient
    {
        private readonly SmtpClient client;

        /// <summary>
        /// Initializes a new instance of the <see cref="GoogleSmtpClient"/> class.
        /// </summary>
        /// <param name="credentials">The credentials.</param>
        /// <param name="ssl">if set to <c>true</c> [SSL].</param>
        public GoogleSmtpClient(NetworkCredential credentials, bool ssl)
        {
            this.client = new SmtpClient("smtp.google.com", ssl ? 587 : 25);
            this.client.EnableSsl = ssl;
            this.client.Credentials = credentials;
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            this.client.Dispose();
        }

        /// <summary>
        /// Sends the mail asynchronous.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>Void result.</returns>
        public Task SendMailAsync(MailMessage message)
        {
            return this.client.SendMailAsync(message);
        }
    }
}
