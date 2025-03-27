using MediatR;
using Yenilen.Application.DTOs;

namespace Yenilen.Application.Features.Booking.Queries;

public class GetCategoriesByStoreIdQuery:IRequest<List<CategoryDto>>
{
    public int StoreId { get; set; }

    public GetCategoriesByStoreIdQuery(int storeId)
    {
        StoreId = storeId;
    }
}