using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.Notification;
using Services.Notification.Intefraces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Football.Core.Extensions
{
    public static class ServicesConfigurationExtesions
    {
        public static void AddSmtpClient(this IServiceCollection services)
        {
            services.AddScoped<ISmtpClient>(sp =>
            {
                var config = sp.GetRequiredService<IConfiguration>();
                var credentials = new NetworkCredential(config.GetValue<string>("GoogleSmtpOptions:MailLogin"), config.GetValue<string>("GoogleSmtpOptions:MailPassword"));
                var enableSsl = config.GetValue<bool>("GoogleSmtpOptions:EnableSsl");

                return new GoogleSmtpClient(credentials, enableSsl);
            });
        }
    }
}
