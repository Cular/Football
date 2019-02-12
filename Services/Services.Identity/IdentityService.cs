using Data.Repository.Interfaces;
using Football.Core.Authorization.Claims;
using Football.Core.Extensions;
using Football.Exceptions;
using Models.Data;
using Models.Infrastructure;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly IPlayerRepository playerRepository;
        private readonly IRefreshTokenRepository refreshTokenRepository;
        private readonly TokenConfiguration configuration;

        public IdentityService(IPlayerRepository playerRepository, IRefreshTokenRepository refreshTokenRepository, TokenConfiguration configuration)
        {
            this.playerRepository = playerRepository;
            this.refreshTokenRepository = refreshTokenRepository;
            this.configuration = configuration;
        }

        public async Task<(string, string, int)> GetTokenAsync(string alias, string password)
        {
            var player = await this.playerRepository.GetAsync(alias) ?? throw new NotFoundException($"Player with alias {alias} not found.");

            if (!player.Active)
            {
                throw new BaseException($"Player with alias {alias} is inactive. Contact to developers.");
            }

            if (!string.Equals(player.PasswordHash, PasswordHasher.GetPasswordHash(password), StringComparison.InvariantCulture))
            {
                throw new BaseException($"Wrond alias or password.");
            }

            var token = JwtAuthorization.GenerateToken(player, this.configuration);
            var refresh = JwtAuthorization.GenerateRefreshToken(this.configuration);

            await this.refreshTokenRepository.RevokeUsersTokensAsync(player.Id);
            await this.refreshTokenRepository.CreateAsync(new RefreshToken
            {
                Id = refresh.refreshTokenKey,
                Active = true,
                Token = refresh.refreshTokenValue,
                UserId = player.Id
            });

            return (token, refresh.refreshTokenKey, (int)this.configuration.Expiration.TotalSeconds);
        }

        public async Task<(string, string, int)> GetTokenAsync(string refreshToken)
        {
            var refreshentity = await this.refreshTokenRepository.GetAsync(refreshToken) ?? throw new NotFoundException();

            if (!refreshentity.Active)
            {
                throw new TokenInactiveException($"Token is inactive. You should relogin");
            }

            var tokenentity = new JwtSecurityTokenHandler().ReadJwtToken(refreshentity.Token);

            if (tokenentity.ValidTo < DateTime.UtcNow)
            {
                throw new TokenExpiredException($"Token is expired. You should relogin");
            }

            var alias = tokenentity.Claims.FirstOrDefault(t => t.Type == FootballClaims.PlayerAlias)?.Value;
            if (string.IsNullOrEmpty(alias))
            {
                throw new NotFoundException("Not found alias.");
            }

            var player = await this.playerRepository.GetAsync(alias);

            if (player == null)
            {
                throw new NotFoundException("Not found player.");
            }

            var token = JwtAuthorization.GenerateToken(player, this.configuration);
            var refresh = JwtAuthorization.GenerateRefreshToken(this.configuration);

            await this.refreshTokenRepository.RevokeUsersTokensAsync(player.Id);
            await this.refreshTokenRepository.CreateAsync(new RefreshToken
            {
                Id = refresh.refreshTokenKey,
                Active = true,
                Token = refresh.refreshTokenValue,
                UserId = player.Id
            });

            return (token, refresh.refreshTokenKey, (int)this.configuration.Expiration.TotalSeconds);
        }
    }
}
