using AutoMapper;
using MediatR;
using TS.Result;
using Yenilen.Application.DTOs;
using Yenilen.Application.Features.Tag.Queries;
using Yenilen.Application.Interfaces;

namespace Yenilen.Application.Features.Tag.Handler;

public class GetAllTagsHandler:IRequestHandler<GetAllTagsQuery,Result<List<GetAllTagsQueryResponse>>>
{
    private readonly ITagRepository _tagRepository;
    private readonly IMapper _mapper;

    public GetAllTagsHandler(ITagRepository tagRepository, IMapper mapper)
    {
        _tagRepository = tagRepository;
        _mapper = mapper;
    }
    
    public async Task<Result<List<GetAllTagsQueryResponse>>> Handle(GetAllTagsQuery request, CancellationToken cancellationToken)
    {

        var tags = _tagRepository.GetAll();

        var tagDtos = _mapper.Map<List<GetAllTagsQueryResponse>>(tags);

        return Result<List<GetAllTagsQueryResponse>>.Succeed(tagDtos);
    }
}