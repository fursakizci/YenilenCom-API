using AutoMapper;
using MediatR;
using Yenilen.Application.DTOs;
using Yenilen.Application.Features.Category.Queries;
using Yenilen.Application.Interfaces;

namespace Yenilen.Application.Features.Category.Handlers;

internal sealed class GetCategoriesByStoreIdHandler: IRequestHandler<GetCategoriesByStoreIdQuery,List<CategoryDto>>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public GetCategoriesByStoreIdHandler(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }
    
    public Task<List<CategoryDto>> Handle(GetCategoriesByStoreIdQuery request, CancellationToken cancellationToken)
    {
        var query = _categoryRepository.Where(x => x.StoreId == request.StoreId).ToList();

        var categories = _mapper.Map<List<CategoryDto>>(query);
        return Task.FromResult(categories);
    }
}