using System;
using System.Threading.Tasks;
using Data.Repository.Interfaces;
using Football.Core.Extensions;
using Models.Data;
using Models.Notification;
using Services.Notification.Intefraces;

namespace Services.Registration
{
    public class RegisterNotifier : IRegisterNotifier
    {
        private readonly INotificationService notificationService;
        private readonly IPlayerActivationRepository activationRepository;

        public RegisterNotifier(INotificationService notificationService, IPlayerActivationRepository activationRepository)
        {
            this.notificationService = notificationService ?? throw new ArgumentNullException(nameof(notificationService));
            this.activationRepository = activationRepository ?? throw new ArgumentNullException(nameof(activationRepository));
        }

        public async Task SendRegisterInfo(Player player)
        {
            var code = await this.CreateActivation(player.Id);
            var message = GetMessage(player.Email, code);

            await this.notificationService.Notify(message);
        }

        private Message GetMessage(string email, string code)
        {
            var message = new Message();
            message.UserId = email;
            message.Title = "Welcome to Football!";
            message.Text = $"Dear user we are happy to invite you to our application. Here is registration code: {code}";

            return message;
        }

        private async Task<string> CreateActivation(string playerId)
        {
            var activation = new PlayerActivation { Id = CodeGenerator.GetCode(), PlayerId = playerId };
            await activationRepository.CreateAsync(activation);

            return activation.Id;
        }
    }
}
