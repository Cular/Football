using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.Notification;
using Services.Notification.Interfaces;
using System.Net;

namespace Football.Core.Extensions
{
    public static class SmtpConfigurations
    {
        public static void AddSmtpClient(this IServiceCollection services)
        {
            services.AddScoped<ISmtpClient>(sp =>
            {
                var configuration = sp.GetRequiredService<IConfiguration>();
                var credentials = new NetworkCredential(configuration.GetValue<string>("GoogleSmtpOptions:MailLogin"), configuration.GetValue<string>("GoogleSmtpOptions:MailPassword"));
                var enableSsl = configuration.GetValue<bool>("GoogleSmtpOptions:EnableSsl");
                var host = configuration.GetValue<string>("GoogleSmtpOptions:Host");
                var port = configuration.GetValue<int>("GoogleSmtpOptions:Port");

                return new GoogleSmtpClient(host, port, credentials, enableSsl);
            });
        }
    }
}
