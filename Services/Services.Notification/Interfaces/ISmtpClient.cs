using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Services.Notification.Intefraces
{
    internal interface ISmtpClient
    {
        string Host { get; }
        int Port { get; set; }
        bool UseSsl { get; set; }
        NetworkCredential Credentials { get; set; }
        SmtpClient Client { get; set; }
    }
}
