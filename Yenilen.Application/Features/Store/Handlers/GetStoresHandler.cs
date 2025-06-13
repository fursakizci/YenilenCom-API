using AutoMapper;
using MediatR;
using TS.Result;
using Yenilen.Application.Features.Store.Queries;
using Yenilen.Application.Interfaces;

namespace Yenilen.Application.Features.Store.Handlers;

internal sealed class GetStoresHandler : IRequestHandler<GetStoresQuery, Result<List<GetStoresQueryResponse>>>
{
    private readonly IStoreRepository _storeRepository;
    private readonly IMapper _mapper;

    public GetStoresHandler(
        IStoreRepository storeRepository,
        IMapper mapper)
    {
        _storeRepository = storeRepository;
        _mapper = mapper;
    }
    
    public async Task<Result<List<GetStoresQueryResponse>>> Handle(GetStoresQuery request, CancellationToken cancellationToken)
    {
        var stores = await _storeRepository.SearchStoresAsync(request.TagId, request.Latitude, request.Longitude, request.Date, cancellationToken);
        
        var responseList = _mapper.Map<List<GetStoresQueryResponse>>(stores);
        
        return Result<List<GetStoresQueryResponse>>.Succeed(responseList);
    }
}