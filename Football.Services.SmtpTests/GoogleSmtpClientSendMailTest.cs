using Microsoft.Extensions.Configuration;
using Models.Notification;
using NUnit.Framework;
using Services.Notification;
using System;
using System.Net;

namespace Football.Services.SmtpTests
{
    class GoogleSmtpClientSendMailTest
    {
        private readonly EmailService _emailService;

        public GoogleSmtpClientSendMailTest()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var credentials = new NetworkCredential(configuration.GetValue<string>("GoogleSmtpOptions:MailLogin"), configuration.GetValue<string>("GoogleSmtpOptions:MailPassword"));
            var enableSsl = configuration.GetValue<bool>("GoogleSmtpOptions:EnableSsl");
            var host = configuration.GetValue<string>("GoogleSmtpOptions:Host");
            var port = configuration.GetValue<int>("GoogleSmtpOptions:Port");

            var googleSmtpClient = new GoogleSmtpClient(host, port, credentials, enableSsl);
            this._emailService = new EmailService(googleSmtpClient);
        }

        [Test]
        public void SendMail()
        {
            var emailAdressToSend = "email@gmail.com";

            var message = new Message()
            {
                Title = "TestTitle",
                Text = "TestText",
                UserId = emailAdressToSend
            };

            var email = this._emailService.Notify(message);



        }
    }
}
