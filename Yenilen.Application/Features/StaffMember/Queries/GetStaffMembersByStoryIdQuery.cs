using MediatR;
using TS.Result;
using Yenilen.Application.DTOs;

namespace Yenilen.Application.Features.StaffMember.Queries;

public class GetStaffMembersByStoryIdQuery:IRequest<Result<List<GetStaffMembersByStoryIdQueryResponse>>>
{
    public int StoreId { get; set; }
}

public sealed class GetStaffMembersByStoryIdQueryResponse
{
    public int StoreId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime? BirthOfDate { get; set; }
    public string StartDate { get; set; }
    public string? Bio { get; set; }
    public string ImageUrl { get; set; }
}