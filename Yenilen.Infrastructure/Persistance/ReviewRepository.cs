using Microsoft.EntityFrameworkCore;
using Yenilen.Application.Interfaces;
using Yenilen.Domain.Entities;
using Yenilen.Infrastructure.DataAccess;

namespace Yenilen.Infrastructure.Persistance;

public class ReviewRepository:GenericRepository<Review>,IReviewRepository
{
    private readonly AppDbContext _context;
    private readonly DbSet<Review> _dbSet;
    
    public ReviewRepository(AppDbContext context) : base(context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _dbSet = _context.Set<Review>();
    }

    public async Task<int> GetReviewCountByStoreId(int id)
    {
        return await _dbSet.CountAsync(r => r.StoreId == id);
    }

    public async Task<decimal> GetStoreRatingByStoreId(int id)
    {
        var hasAny = await _context.Reviews.AnyAsync(r => r.StoreId == id);

        if (!hasAny)
            return 0;
        
        var avgRating = await _context.Reviews
            .Where(r => r.StoreId == id)
            .AverageAsync(r => r.Rating);

        return avgRating;
    }
}