using Microsoft.EntityFrameworkCore;
using Yenilen.Application.DTOs;
using Yenilen.Application.Interfaces;
using Yenilen.Domain.DTOs;
using Yenilen.Domain.Entities;
using Yenilen.Infrastructure.DataAccess;

namespace Yenilen.Infrastructure.Persistance;

internal sealed class StoreRepository:GenericRepository<Store, AppDbContext>,IStoreRepository
{
    private readonly AppDbContext _context;
    private readonly DbSet<Store> _dbSet;
    
    public StoreRepository(AppDbContext context) : base(context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _dbSet = _context.Set<Store>();
    }

    public async Task<string> GetStoreFullAddressById(int id)
    {
        var store = await _dbSet.Include(
                s => s.Address)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (store == null || store.Address == null)
            return string.Empty;

        return store.Address.FullAddress;
    }

    public async Task<StoreDashboardDto> GetStoreWithDetailsAsync(int id)
    {
        var storeDetails = await _dbSet.Where(s => s.Id == id)
            .Select(s => new StoreDashboardDto
            {
                Id = s.Id,
                StoreName = s.StoreName,
                Address = s.Address,
                Images = s.Images,
                Categories = s.Categories.Select(c => new Category
                {
                    Id = c.Id,
                    Name = c.Name,
                    Services = c.Services.Select(se => new Service
                    {
                        CategoryId = se.CategoryId,
                        Id = se.Id,
                        Name = se.Name,
                        Price = se.Price,
                        Duration = se.Duration
                    }).ToList()
                }).ToList(),
                StoreWorkingHours = s.WorkingHours.Select(sw => new StoreWorkingHour
                {
                    OpeningTime = sw.OpeningTime,
                    ClosingTime = sw.ClosingTime,
                    DayOfWeek = sw.DayOfWeek,
                    IsClosed = sw.IsClosed
                }).ToList(),
                StaffMembers = s.StaffMembers.Select(staff => new Staff
                {
                    Id = staff.Id,
                    FirstName = staff.FirstName,
                    LastName = staff.LastName,
                    Image = staff.Image
                }).ToList(),
                About = s.About,
                Reviews = s.Reviews.OrderByDescending(r => r.CreatedAt)
                    .Take(10)
                    .Select(r => new Review
                    {
                        Id = r.Id,
                        Text = r.Text,
                        CreatedAt = r.CreatedAt
                    }).ToList()
            }).FirstOrDefaultAsync();
        return storeDetails;
        
        // var store = await _dbSet.Where(s => s.Id == id)
        //     .Include(s => s.Address)
        //     .Include(s => s.Reviews)
        //     .Include(s => s.Categories)
        //     .ThenInclude(c => c.Services)
        //     .Include(s => s.Images)
        //     .Include(s => s.WorkingHours)
        //     .FirstOrDefaultAsync();
        //
        // return store ;
    }

    public async Task<Store> GetStoreWithWorkingTimesAsync(int id)
    {
        var store = await _dbSet.Where(s => s.Id == id)
            .Include(s => s.WorkingHours)
            .FirstOrDefaultAsync();

        return store;
    }

    public async Task<Store?> GetStoreByUserIdAsync(Guid? appUserId)
    {
        return await _dbSet.Include(s => s.StoreOwner).FirstOrDefaultAsync(s => s.StoreOwner.AppUserId == appUserId);
    }

    public async Task<Store?> GetStoreWithCategoriesByUserId(Guid? appUserId, CancellationToken cancellationToken)
    {
        var store = await _dbSet.Include(s => s.Categories)
            .FirstOrDefaultAsync(s => s.StoreOwner.AppUserId == appUserId, cancellationToken);

        return store;
    }

    public async Task<IEnumerable<StoreSearchDto>?> SearchStoresByNameAsync(string? query, int maxResutls, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(query))
            return Enumerable.Empty<StoreSearchDto>();

        return await _dbSet.Where(s => s.StoreName.ToLower().Contains(query.ToLower()))
            .Include(s => s.Images)
            .Include(s => s.Address)
            .OrderBy(s => s.StoreName)
            .Take(maxResutls)
            .Select(s => new StoreSearchDto
            {
                StoreId = s.Id,
                StoreName = s.StoreName,
                FullAddress = s.Address.FullAddress,
                ImageUrl = s.Images.OrderBy(i => i.ImageUrl).Select(i => i.ImageUrl).FirstOrDefault()
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<StoreSearchResultDto>> SearchStoresAsync(int? tagId, double latitude, double longitude, DateTime? date,
        CancellationToken cancellationToken)
    {

        const double  halfDiameterEarth = 6371;

        var query = _dbSet.AsQueryable();

        if (tagId.HasValue)
        {
            query = query.Where(s => s.Tags.Any(tag => tag.Id == tagId.Value));
        }

        if (date.HasValue)
        {
            var dayOfWeek = (int)date.Value.DayOfWeek;
            query = query.Where(s => s.WorkingHours.Any(wh => (int)wh.DayOfWeek == dayOfWeek && wh.IsClosed == false));
        }

        query = query.Where(s => s.Address.Latitude != null && s.Address.Longitude != null);
        
        

        var result = await query
            .Select(s => new StoreSearchResultDto
            {
                StoreId = s.Id.ToString(),
                Name = s.StoreName,
                Rating = s.Reviews.Any() ? (double?)s.Reviews.Average(r => r.Rating) : null,
                CountOfReview = s.Reviews.Count,
                ImageUrl = s.Images.OrderBy(i => i.Id).Select(i => i.ImageUrl).FirstOrDefault(),
                Distance = halfDiameterEarth * Math.Acos(
                    Math.Cos(latitude * Math.PI / 180) * Math.Cos(s.Address.Latitude * Math.PI / 180) *
                    Math.Cos(s.Address.Longitude * Math.PI / 180 - longitude * Math.PI / 180) +
                    Math.Sin(latitude * Math.PI / 180) * Math.Sin(s.Address.Latitude * Math.PI / 180)
                ),
                FullAddress = s.Address.FullAddress,
                Longitude = s.Address.Longitude,
                Latitude = s.Address.Latitude,
                Services = s.Categories
                    .SelectMany(c => c.Services)
                    .OrderBy(service => service.Id)
                    .Take(3)
                    .Select(s => new Domain.DTOs.ServiceDto
                    {
                        CategoryId =  s.CategoryId.ToString(),
                        ServiceId = s.Id.ToString(),
                        Name = s.Name,
                        Price = s.Price,
                        Duration = s.Duration
                    }).ToList()
            })
            .OrderBy(x => x.Distance)
            .ToListAsync(cancellationToken);

        return result;
    }
}