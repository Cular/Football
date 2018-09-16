using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
            services.AddScoped<SmtpClient>(sp =>
            {
                var config = sp.GetRequiredService<IConfiguration>();

                return new SmtpClient
                {
                    Host = config.GetValue<string>("SmtpOptions:Host"),
                    Port = config.GetValue<int>("SmtpOptions:Port"),
                    EnableSsl = config.GetValue<bool>("SmtpOptions:EnableSsl"),
                    Credentials = new NetworkCredential(config.GetValue<string>("SmtpOptions:MailLogin"), config.GetValue<string>("SmtpOptions:MailPassword"))
                };
            });
        }
    }
}
