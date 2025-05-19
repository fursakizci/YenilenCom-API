using MediatR;
using TS.Result;
using Yenilen.Application.DTOs;

namespace Yenilen.Application.Features.Store.Queries;

public sealed class GetStoreByIdQuery:IRequest<Result<GetStoreByIdQueryResponse>>
{
    public int StoreId { get; set; }
}

public sealed class GetStoreByIdQueryResponse
{
    public string Name { get; set; }
    public AddressDto Address { get; set; }
    public List<ImageDto> Images { get; set; }
    public List<StoreWorkingHourDto> StoreWorkingHours { get; set; }
    public decimal Rating { get; set; }
    public List<ReviewDto> Reviews { get; set; }
    public List<CategoryDto> Categories { get; set; }
    public List<StaffDto> StaffMembers { get; set; }
    public string? About { get; set; }
}