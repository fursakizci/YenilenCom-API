using Yenilen.Domain.DTOs;
using Yenilen.Domain.Entities;

namespace Yenilen.Application.Interfaces;

public interface IStaffRepository:IGenericRepository<Staff>
{
    Task<IEnumerable<Staff>> GetStaffMembersByStoreIdAsync(int id);
    Task<bool> StaffExistsByPhoneNumberAsync(string phoneNumber = "", string email = "");
}