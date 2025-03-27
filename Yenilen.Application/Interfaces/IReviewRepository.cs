using Yenilen.Domain.Entities;

namespace Yenilen.Application.Interfaces;

public interface IReviewRepository:IGenericRepository<Review>
{
    Task<int> GetReviewCountByStoreId(int id);
    Task<float> GetStoreRatingByStoreId(int id);

}