using Yenilen.Domain.Entities;
using Yenilen.Domain.Users;

namespace Yenilen.Application.Interfaces;

public interface ICustomerRepository:IGenericRepository<Customer>
{
    Task<bool> CustomerExistsByPhoneNumberAsync(string phoneNumber = "", string email = "");
    Task<Customer?> GetByIdAsync(Guid? userId, bool includeRelated = false);
    Task<Customer?> GetFavouriteByIdAsync(int id);
    Task<Customer?> GetCustomerByGuid(Guid? userId);
    Task<Customer?> GetCustomerWithAvatarAndAddressesAsync(Guid? userId, CancellationToken cancellationToken);
}