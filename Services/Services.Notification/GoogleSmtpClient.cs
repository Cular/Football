using Services.Notification.Intefraces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Services.Notification
{
    class GoogleSmtpClient : ISmtpClient
    {
        public string Host { get => "smtp.gmail.com"; }
        public int Port { get; set; }
        public bool UseSsl { get; set; }
        public NetworkCredential Credentials { get; set; }
        public SmtpClient Client { get; set; }

        public GoogleSmtpClient(NetworkCredential credentials, bool ssl)
        {
            Credentials = credentials;
            Port = 25;
            UseSsl = ssl;
            if (UseSsl)
            {
                Port = 587;
            }

            Client = new SmtpClient(Host, Port);
        }

        public void Dispose()
        {
            Client.Dispose();
        }
    }
}
