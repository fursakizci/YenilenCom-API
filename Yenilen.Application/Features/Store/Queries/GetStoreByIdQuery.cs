using MediatR;
using Yenilen.Application.DTOs;

namespace Yenilen.Application.Features.Store.Queries;

public sealed class GetStoreByIdQuery:IRequest<StoreIndividualDto>
{
    public int StoreId { get; set; }
}