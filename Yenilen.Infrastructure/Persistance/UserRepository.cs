using Microsoft.EntityFrameworkCore;
using Yenilen.Application.Interfaces;
using Yenilen.Domain.Entities;
using Yenilen.Infrastructure.DataAccess;

namespace Yenilen.Infrastructure.Persistance;

public class UserRepository:GenericRepository<User>,IUserRepository
{
    private readonly AppDbContext _context;
    private readonly DbSet<User> _dbSet;
    
    public UserRepository(AppDbContext context) : base(context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _dbSet = _context.Set<User>();
    }

    public async Task<bool> IsExistsAsync(string phoneNumber, string email)
    {
        return await _dbSet.AnyAsync(i => i.Email == email || i.PhoneNumber == phoneNumber);
    }
}