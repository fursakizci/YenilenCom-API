using MediatR;
using TS.Result;
using Yenilen.Application.DTOs;

namespace Yenilen.Application.Auth.Queries;

public sealed class MeCustomerQuery : IRequest<Result<MeCustomerQueryResponse>>
{
    
}

public sealed class MeCustomerQueryResponse
{
    public string Id { get; set; }
    public string FullName { get; set; }
    public string? Email { get; set; } 
    public string? AvatarUrl { get; set; } = string.Empty;
    public IReadOnlyList<AddressDto>? Addresses { get; set; }
}