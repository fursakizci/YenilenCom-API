using MediatR;
using Yenilen.Application.DTOs;

namespace Yenilen.Application.Features.Booking.Queries;

public class GetStaffMembersByStoryIdQuery:IRequest<List<StaffDto>>
{
    public int StoreId { get; set; }

    public GetStaffMembersByStoryIdQuery(int storeId)
    {
        StoreId = storeId;
    }
}