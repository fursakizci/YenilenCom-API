using AutoMapper;
using MediatR;
using TS.Result;
using Yenilen.Application.DTOs;
using Yenilen.Application.Features.Search.Queries;
using Yenilen.Application.Interfaces;

namespace Yenilen.Application.Features.Search.Handler;

internal sealed class GetStoresAndTagsHandler : IRequestHandler<GetStoresAndTagsQuery,Result<GetStoresAndTagsResponse>>
{
    private readonly ITagRepository _tagRepository;
    private readonly IStoreRepository _storeRepository;
    private readonly IMapper _mapper;
    
    public GetStoresAndTagsHandler(ITagRepository tagRepository,
        IStoreRepository storeRepository,
        IMapper mapper)
    {
        _tagRepository = tagRepository;
        _storeRepository = storeRepository;
        _mapper = mapper;
    }
    
    public async Task<Result<GetStoresAndTagsResponse>> Handle(GetStoresAndTagsQuery request, CancellationToken cancellationToken)
    {
        var tags = await _tagRepository.SearchTagsByNameAsync(request.QueryString, 5, cancellationToken);
        var stores = await _storeRepository.SearchStoresByNameAsync(request.QueryString, 8, cancellationToken);
        
        var tagDtos = _mapper.Map<List<TagDto>>(tags);
        
        var response = new GetStoresAndTagsResponse
        {   
            Tags = tagDtos,
            Stores = stores.ToList()
        };

        return Result<GetStoresAndTagsResponse>.Succeed(response);
    }
}   