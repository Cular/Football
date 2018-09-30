using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Notification
{
    class SmtpSettings
    {
        public string MailLogin { get; private set; }
        public string MailPassword { get; private set; }
        public string Host { get; private set; }
        public int Port { get; private set; }
        public bool EnableSsl { get; private set; }


        // TODO: Доделать получение конфигураций из класса
        public SmtpSettings()
        {
            var settings = configuration.GetSection("SmtpOptions");

            MailLogin = settings.GetSection("MailLogin").ToString();
            MailPassword = settings.GetSection("MailPassword").ToString();
            Host = settings.GetSection("Host").ToString();
            Port = int.Parse(settings.GetSection("Port").ToString());
            EnableSsl = bool.Parse(settings.GetSection("Port").ToString());
        }
    }
}
