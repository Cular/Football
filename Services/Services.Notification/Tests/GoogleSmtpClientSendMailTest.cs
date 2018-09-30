using Microsoft.Extensions.Options;
using Models.Notification;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Security;
using System.Text;

namespace Services.Notification.Tests
{
    class GoogleSmtpClientSendMailTest
    {
        private readonly GoogleSmtpClient _googleSmtpClient;
        private readonly EmailService _emailService;


        public GoogleSmtpClientSendMailTest()
        {
            var credentials = new NetworkCredential("", "");
            this._googleSmtpClient = new GoogleSmtpClient(credentials, true);
            var smtpClient = this._googleSmtpClient.GetClient();

            // Как мне лучше достать клиент, чтобы передать его в твой класс.
            this._emailService = new EmailService(smtpClient);
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
