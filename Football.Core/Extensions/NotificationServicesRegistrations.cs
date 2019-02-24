using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.Notification;
using Services.Notification.Interfaces;
using System;
using System.Net.Http;

namespace Football.Core.Extensions
{
    public static class NotificationServicesRegistrations
    {
        public static IServiceCollection AddEmailService(this IServiceCollection services)
        {
            return services.AddScoped<IEmailNotificationService, EmailService>();
        }

        public static IServiceCollection AddFirebaseService(this IServiceCollection services)
        {
            services.AddScoped<IPushNotificationService, FirebasePushService>(sp =>
            {
                var key = sp.GetService<IConfiguration>().GetSection("FirebaseOptions").GetValue<string>("Key");
                var httpclient = sp.GetService<HttpClient>();
                httpclient.BaseAddress = new Uri("https://fcm.googleapis.com/");
                httpclient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("key=", key);

                return new FirebasePushService(httpclient);
            });

            return services;
        }
    }
}
