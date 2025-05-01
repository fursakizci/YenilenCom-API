using MediatR;
using Yenilen.Application.DTOs;

namespace Yenilen.Application.Features.Service.Queries;

public class GetServicesByCategoryIdQuery:IRequest<List<ServiceDto>>
{
    public int CategoryId { get; set; }
    
}