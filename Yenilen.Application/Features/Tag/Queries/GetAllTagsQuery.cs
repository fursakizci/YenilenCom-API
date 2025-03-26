using MediatR;
using Yenilen.Application.DTOs;

namespace Yenilen.Application.Features.Tag.Queries;

public class GetAllTagsQuery :IRequest<List<TagDto>>
{
    
}