using AutoMapper;
using MediatR;
using Yenilen.Application.DTOs;
using Yenilen.Application.Features.Tag.Queries;
using Yenilen.Application.Interfaces;

namespace Yenilen.Application.Features.Tag.Handler;

public class GetAllTagsHandler:IRequestHandler<GetAllTagsQuery,List<TagDto>>
{
    private readonly ITagRepository _tagRepository;
    private readonly IMapper _mapper;

    public GetAllTagsHandler(ITagRepository tagRepository, IMapper mapper)
    {
        _tagRepository = tagRepository;
        _mapper = mapper;
    }
    
    public async Task<List<TagDto>> Handle(GetAllTagsQuery request, CancellationToken cancellationToken)
    {

        var tags = _tagRepository.GetAll();

        var tagDtos = _mapper.Map<List<TagDto>>(tags);

        return tagDtos;
    }
}