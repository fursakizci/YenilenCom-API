using AutoMapper;
using MediatR;
using Yenilen.Application.DTOs;
using Yenilen.Application.Features.Booking.Queries;
using Yenilen.Application.Interfaces;

namespace Yenilen.Application.Features.Booking.Handlers;

public class GetCategoriesByStoreIdHandler:IRequestHandler<GetCategoriesByStoreIdQuery,List<CategoryDto>>
{
    private ICategoryRepository _categoryRepository;
    private IMapper _mapper;

    public GetCategoriesByStoreIdHandler(ICategoryRepository categoryRepository,IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }
    
    public async Task<List<CategoryDto>> Handle(GetCategoriesByStoreIdQuery request, CancellationToken cancellationToken)
    {
        var categories = await _categoryRepository.GetAllCategoriesByStoreId(request.StoreId);
        
        if (categories == null || !categories.Any())
        return new List<CategoryDto>();
        
        var categoriesDto = _mapper.Map<List<CategoryDto>>(categories);
        
        return categoriesDto;
    }
}