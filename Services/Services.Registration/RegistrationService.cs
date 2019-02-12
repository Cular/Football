using System;
using System.Threading.Tasks;
using Data.Repository.Interfaces;
using Football.Exceptions;
using Models.Data;

namespace Services.Registration
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IPlayerActivationRepository activationRepository;
        private readonly IRegisterNotifier notifier;
        private readonly IPlayerRepository playerRepository;

        public RegistrationService(IPlayerActivationRepository activationRepository, IRegisterNotifier notifier, IPlayerRepository playerRepository)
        {
            this.activationRepository = activationRepository;
            this.notifier = notifier;
            this.playerRepository = playerRepository;
        }

        public async Task ActivateAccountAsync(string code)
        {
            var activation = await this.activationRepository.GetAsync(code) ?? throw new NotFoundException($"Activation code {code} not found.");

            activation.Player.Active = true;

            await this.playerRepository.UpdateAsync(activation.Player);
            await this.activationRepository.DeleteAsync(activation);
        }

        public Task CreatePlayerAsync(Player player)
        {
            var tasks = new Task[]
            {
                this.playerRepository.CreateAsync(player),
#if !DEBUG
                this.notifier.SendRegisterInfo(player)
#endif
            };

            return Task.WhenAll(tasks);
        }
    }
}
