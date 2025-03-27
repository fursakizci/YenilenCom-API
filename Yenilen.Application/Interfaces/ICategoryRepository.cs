using Yenilen.Domain.Entities;

namespace Yenilen.Application.Interfaces;

public interface ICategoryRepository:IGenericRepository<Category>
{
    Task<IEnumerable<Category>> GetAllCategoriesByStoreId(int id);
}