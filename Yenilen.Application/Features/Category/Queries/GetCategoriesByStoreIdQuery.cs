using MediatR;
using Yenilen.Application.DTOs;

namespace Yenilen.Application.Features.Category.Queries;

public sealed class GetCategoriesByStoreIdQuery: IRequest<List<CategoryDto>>
{
    public int StoreId { get; set; }
}