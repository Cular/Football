using Services.Notification.Intefraces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Services.Notification
{
    class GoogleSmtpClient : ISmtpClient, IDisposable
    {
        private readonly SmtpSettings _smtpSettings;

        public string Host { get; set; }
        public int Port { get; set; }
        public bool UseSsl { get; set; }
        public NetworkCredential Credentials { get; set; }

        private SmtpClient Client { get; set; }

        public GoogleSmtpClient(NetworkCredential credentials, bool ssl)
        {
            Port = ssl ? 587 : 25;
            UseSsl = ssl;
            Credentials = credentials;

            Client = new SmtpClient(Host, Port);
            Client.EnableSsl = true;
            Client.Credentials = credentials;
        }

        public void Dispose()
        {
            Client.Dispose();
        }

        // TODO: Нужен ли метод, возвращающий клиент? Ибо в тесте я должен передать клиент в класс
        public SmtpClient GetClient()
        {
            return this.Client;
        }
    }
}
