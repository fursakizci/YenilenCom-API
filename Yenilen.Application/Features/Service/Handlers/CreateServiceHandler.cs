using AutoMapper;
using MediatR;
using TS.Result;
using Yenilen.Application.Features.Service.Commands;
using Yenilen.Application.Interfaces;
using Yenilen.Application.Services;
using Yenilen.Application.Services.Common;

namespace Yenilen.Application.Features.Service.Handlers;

internal sealed class CreateServiceHandler : IRequestHandler<CreateServiceCommand,Result<CreateServiceCommandResponse>>
{

    private readonly IServiceRepository _serviceRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IStoreRepository _storeRepository;
    private readonly IStoreOwnerPolicyService _storeOwnerPolicyService;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
        
    public CreateServiceHandler(IServiceRepository serviceRepository,
        ICategoryRepository categoryRepository,
        IStoreRepository storeRepository,
        IStoreOwnerPolicyService storeOwnerPolicyService,
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _serviceRepository = serviceRepository;
        _categoryRepository = categoryRepository;
        _storeRepository = storeRepository;
        _storeOwnerPolicyService = storeOwnerPolicyService;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<CreateServiceCommandResponse>> Handle(CreateServiceCommand request, CancellationToken cancellationToken)
    {
        var appUserId = await _storeOwnerPolicyService.ValidateAndGetAppUserIdAsync(cancellationToken);

        if (!appUserId.IsSuccessful)
        {
            return Result<CreateServiceCommandResponse>.Failure(appUserId.StatusCode, appUserId.ErrorMessages ?? new List<string> { "Kullanıcı ID alınamadı." });
        }

        await _unitOfWork.BeginTransactionAsync(cancellationToken);
        
        try
        {
            var store = await _storeRepository.GetStoreWithCategoriesByUserId(appUserId.Data, cancellationToken);
            
            if (store is null)
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                return Result<CreateServiceCommandResponse>.Failure("Magaza bulunamadi.");
            }
            
            var isExistService = await _serviceRepository.AnyAsync(s => s.Name == request.Name, cancellationToken);

            if (isExistService)
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                return Result<CreateServiceCommandResponse>.Failure("Girdiginiz Servis adina ait servisiniz bulunmaktadir");
            }

            var isCategoryExist = store.Categories.Any(s => s.Id == request.CategoryId);
            var category = await _categoryRepository.GetByIdAsync(request.CategoryId);
            
            if (!isCategoryExist || category is null)
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                return Result<CreateServiceCommandResponse>.Failure("Belirtilen kategori bulunamadi.");
            }

            var service = _mapper.Map<Domain.Entities.Service>(request);

            await _serviceRepository.AddAsync(service, cancellationToken);
            
            category.Services.Add(service);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitTransactionAsync(cancellationToken);

            var result = new CreateServiceCommandResponse()
            {
                ServiceId = service.Id
            };

            return Result<CreateServiceCommandResponse>.Succeed(result);

        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);
            return Result<CreateServiceCommandResponse>.Failure($"Servis kayit islemi gerceklestirilemedi. {ex}");
        }
        
       
    }
}