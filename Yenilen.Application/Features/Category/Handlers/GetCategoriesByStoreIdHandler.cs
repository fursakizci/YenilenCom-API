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
    
    public async Task<Result<List<GetCategoriesByStoreIdQueryResponse>>> Handle(GetCategoriesByStoreIdQuery request, CancellationToken cancellationToken)
    {

        if (!int.TryParse(request.StoreId, out int storeId))
        {
            return Result<List<GetCategoriesByStoreIdQueryResponse>>.Failure("Dogru store id degeri girin.");
        }
        
        var query = await _categoryRepository.GetCategoriesWithServicesByStoreIdAsync(storeId);

        var categories = _mapper.Map<List<GetCategoriesByStoreIdQueryResponse>>(query);

        return Result<List<GetCategoriesByStoreIdQueryResponse>>.Succeed(categories);
    }
}