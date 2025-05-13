using MediatR;
using Yenilen.Application.DTOs;

namespace Yenilen.Application.Features.Profile.Queries;

public class GetProfileByIdQuery:IRequest<ProfileDto>
{
    public int UserId { get; set; }
    
}