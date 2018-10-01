using System;
using System.Threading.Tasks;

namespace Services.Identity
{
    public interface IIdentityService
    {
        Task<(string access, string refresh, int expiration)> GetTokenAsync(string alias, string password);
        Task<(string access, string refresh, int expiration)> GetTokenAsync(string refreshToken);
    }
}
