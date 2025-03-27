using MediatR;
using Yenilen.Application.DTOs;

namespace Yenilen.Application.Features.Booking.Queries;

public class GetServicesByCategoryIdQuery:IRequest<List<ServiceDto>>
{
    public int CategoryId { get; set; }

    public GetServicesByCategoryIdQuery(int categoryId)
    {
        CategoryId = categoryId;
    }
}