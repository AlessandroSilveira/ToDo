﻿using System.Threading.Tasks;
using ToDo.Domain.Auth;

namespace ToDo.Domain.Repositories.Interfaces
{
    public interface IRefreshTokenCacheRepository
    {
        Task Add(RefreshToken refreshToken);
        Task<RefreshToken> GetById(string refreshToken);
    }
}
