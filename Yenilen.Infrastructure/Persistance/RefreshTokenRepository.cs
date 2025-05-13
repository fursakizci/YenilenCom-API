using Microsoft.EntityFrameworkCore;
using Yenilen.Application.Interfaces;
using Yenilen.Domain.Entities;
using Yenilen.Infrastructure.DataAccess;

namespace Yenilen.Infrastructure.Persistance;

internal sealed class RefreshTokenRepository: GenericRepository<RefreshToken, AppDbContext>, IRefreshTokenRepository
{
    
    private readonly AppDbContext _context;
    private readonly DbSet<RefreshToken> _dbSet;
    
    public RefreshTokenRepository(AppDbContext context) : base(context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _dbSet = _context.Set<RefreshToken>();
    }

    public async Task<RefreshToken> GetByToken(string refreshToken)
    {
        var tokenIdDb = await _dbSet.Where(r => r.Token == refreshToken).FirstOrDefaultAsync();
        return tokenIdDb;
    }
}