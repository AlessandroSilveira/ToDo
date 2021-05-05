using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using ToDo.Domain.Auth;
using ToDo.Domain.Repositories.Interfaces;

namespace ToDo.Infra.Repositories
{
    public class RefreshTokenCacheRepository : IRefreshTokenCacheRepository
    {
        private readonly IDistributedCache _cache;

        public RefreshTokenCacheRepository(IDistributedCache cache)
        {
            _cache = cache;
        }

        public void Add(RefreshToken refreshToken)
        {
            TimeSpan finalExpiration =TimeSpan.FromSeconds(120);
            DistributedCacheEntryOptions opcoesCache = new DistributedCacheEntryOptions();
            opcoesCache.SetAbsoluteExpiration(finalExpiration);
            _cache.SetString(refreshToken.TokenString, JsonConvert.SerializeObject(refreshToken),opcoesCache);
        }

        public Task<RefreshToken> GetById(string refreshToken)
        {
            RefreshToken refreshTokenBase = null;
            string strTokenArmazenado =_cache.GetString(refreshToken);

            if (!string.IsNullOrWhiteSpace(strTokenArmazenado))            
                refreshTokenBase = JsonConvert.DeserializeObject<RefreshToken>(strTokenArmazenado);            

            return Task.FromResult(refreshTokenBase);
        }
    }
}
