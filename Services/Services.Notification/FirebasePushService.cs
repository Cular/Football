// <copyright file="FirebasePushService.cs" company="Yarik Home">
// All rights maybe reserved. © Yarik
// </copyright>

namespace Services.Notification
{
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using Models.Notification;
    using Newtonsoft.Json;
    using Services.Notification.Interfaces;

    /// <summary>
    /// The firebase push notification service implementation.
    /// </summary>
    public class FirebasePushService : IPushNotificationService
    {
        private readonly HttpClient httpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="FirebasePushService"/> class.
        /// </summary>
        /// <param name="httpClient">The HTTP client.</param>
        public FirebasePushService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        /// <summary>
        /// Notifies this instance.
        /// </summary>
        /// <param name="message">The message for user.</param>
        /// <returns>
        /// Void result.
        /// </returns>
        public async Task Notify(Message message)
        {
            var sendObject = new { to = message.UserId, notification = new { title = message.Title, body = message.Text } };

            using (var request = new HttpRequestMessage(HttpMethod.Post, "fcm/send"))
            {
                request.Content = new StringContent(JsonConvert.SerializeObject(sendObject), Encoding.UTF8, "application/json");
                (await this.httpClient.SendAsync(request)).EnsureSuccessStatusCode();
            }
        }

        /// <summary>
        /// Notifies the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>
        /// Void result.
        /// </returns>
        public async Task Notify(BatchMessage message)
        {
            var sendObject = new { registration_ids = message.UsersIds, notification = new { title = message.Title, body = message.Text } };

            using (var request = new HttpRequestMessage(HttpMethod.Post, "fcm/send"))
            {
                request.Content = new StringContent(JsonConvert.SerializeObject(sendObject), Encoding.UTF8, "application/json");
                (await this.httpClient.SendAsync(request)).EnsureSuccessStatusCode();
            }
        }
    }
}
