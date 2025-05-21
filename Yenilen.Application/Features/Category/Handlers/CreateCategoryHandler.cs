using AutoMapper;
using MediatR;
using TS.Result;
using Yenilen.Application.Features.Category.Commands;
using Yenilen.Application.Interfaces;
using Yenilen.Application.Services;
using Yenilen.Application.Services.Common;

namespace Yenilen.Application.Features.Category.Handlers;

internal sealed class CreateCategoryHandler:IRequestHandler<CreateCategoryCommand, Result<CreateCategoryCommandResponse>>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IRequestContextService _requestContextService;
    private readonly IStoreRepository _storeRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    
    public CreateCategoryHandler(ICategoryRepository categoryRepository,
        IRequestContextService requestContextService,
        IStoreRepository storeRepository,
        IMapper mapper, IUnitOfWork unitOfWork)
    {
        _categoryRepository = categoryRepository;
        _requestContextService = requestContextService;
        _storeRepository = storeRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<CreateCategoryCommandResponse>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var appUserId = _requestContextService.GetCurrentUserId();

        if (appUserId is null)
        {
            return Result<CreateCategoryCommandResponse>.Failure("Magaza kullaci bilgisine ulasilamadi.");
        }
        
        await _unitOfWork.BeginTransactionAsync(cancellationToken);

        try
        {
            var store = await _storeRepository.GetStoreByUserIdAsync(appUserId);
        
            if (store == null)
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                return Result<CreateCategoryCommandResponse>.Failure("Magaza bilgisine ulasilamadi.");
            }
        
            var isCategoryExist = await _categoryRepository.AnyAsync(x => x.Name == request.Name, cancellationToken);

            if (isCategoryExist)
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                return Result<CreateCategoryCommandResponse>.Failure("Girdiginiz kategori ismine ait kayit bulunmaktadir.");
            }
        
            var category = _mapper.Map<Domain.Entities.Category>(request);

            await _categoryRepository.AddAsync(category);
            await _unitOfWork.SaveChangesAsync(appUserId, cancellationToken);
            await _unitOfWork.CommitTransactionAsync(cancellationToken);

            var result = new CreateCategoryCommandResponse()
            {
                CategoryId = category.Id
            };

            return Result<CreateCategoryCommandResponse>.Succeed(result);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);
            return Result<CreateCategoryCommandResponse>.Failure($"Kategori olusturulamadi. {ex}");
        }
    }
}