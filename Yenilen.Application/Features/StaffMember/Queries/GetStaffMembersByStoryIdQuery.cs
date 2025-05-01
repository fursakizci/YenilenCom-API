using MediatR;
using Yenilen.Application.DTOs;

namespace Yenilen.Application.Features.StaffMember.Queries;

public class GetStaffMembersByStoryIdQuery:IRequest<List<StaffDto>>
{
    public int StoreId { get; set; }
    
}