using AutoMapper;
using MediatR;
using Yenilen.Application.Features.Category.Commands;
using Yenilen.Application.Interfaces;
using Yenilen.Application.Services;

namespace Yenilen.Application.Features.Category.Handlers;

internal sealed class CreateCategoryHandler:IRequestHandler<CreateCategoryCommand, int>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    
    public CreateCategoryHandler(ICategoryRepository categoryRepository,IMapper mapper, IUnitOfWork unitOfWork)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task<int> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var isCategoryExist = await _categoryRepository.AnyAsync(x => x.Name == request.Name, cancellationToken);

        if (isCategoryExist)
            throw new InvalidOperationException("Girdiginiz kategori ismine ait kayit bulunmaktadir.");

        var category = _mapper.Map<Domain.Entities.Category>(request);

        await _categoryRepository.AddAsync(category);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return category.Id;
    }
}