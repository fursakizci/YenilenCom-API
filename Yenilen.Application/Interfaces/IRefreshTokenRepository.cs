using Yenilen.Domain.Entities;

namespace Yenilen.Application.Interfaces;

public interface IRefreshTokenRepository:IGenericRepository<RefreshToken>
{
    Task<RefreshToken> GetByToken(string refreshToken);
}