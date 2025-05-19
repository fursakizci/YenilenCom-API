using AutoMapper;
using MediatR;
using TS.Result;
using Yenilen.Application.DTOs;
using Yenilen.Application.Features.Category.Queries;
using Yenilen.Application.Interfaces;

namespace Yenilen.Application.Features.Category.Handlers;

internal sealed class GetCategoriesByStoreIdHandler: IRequestHandler<GetCategoriesByStoreIdQuery,Result<List<GetCategoriesByStoreIdQueryResponse>>>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public GetCategoriesByStoreIdHandler(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }
    
    public Task<Result<List<GetCategoriesByStoreIdQueryResponse>>> Handle(GetCategoriesByStoreIdQuery request, CancellationToken cancellationToken)
    {
        var query = _categoryRepository.Where(x => x.StoreId == request.StoreId).ToList();

        var categories = _mapper.Map<List<GetCategoriesByStoreIdQueryResponse>>(query);
        
        return Task.FromResult(Result<List<GetCategoriesByStoreIdQueryResponse>>.Succeed(categories));
    }
}