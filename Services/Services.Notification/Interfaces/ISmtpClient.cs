using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Services.Notification.Intefraces
{
    public interface ISmtpClient
    {
        string Host { get; }
        int Port { get; }
        bool UseSsl { get; }
        NetworkCredential Credentials { get; }
    }
}
