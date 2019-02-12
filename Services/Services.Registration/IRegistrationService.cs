using Models.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Registration
{
    public interface IRegistrationService
    {
        Task CreatePlayerAsync(Player player);

        Task ActivateAccountAsync(string code);
    }
}
