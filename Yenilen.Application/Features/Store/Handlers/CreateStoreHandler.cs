using AutoMapper;
using MediatR;
using TS.Result;
using Yenilen.Application.Features.Store.Commands;
using Yenilen.Application.Interfaces;
using Yenilen.Application.Services;
using Yenilen.Application.Services.Common;

namespace Yenilen.Application.Features.Store.Handlers;

internal sealed class CreateStoreHandler:IRequestHandler<CreateStoreCommand, Result<CreateStoreCommandResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IStoreRepository _storeRepository;
    private readonly ITagRepository _tagRepository;
    private readonly IStoreOwnerRepository _storeOwnerRepository;
    private readonly IRequestContextService _requestContextService;
    private readonly IMapper _mapper;
    
    public CreateStoreHandler(IUnitOfWork unitOfWork, 
        IStoreRepository storeRepository, 
        ITagRepository tagRepository, 
        IStoreOwnerRepository storeOwnerRepository,
        IRequestContextService requestContextService,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _storeRepository = storeRepository;
        _tagRepository = tagRepository;
        _storeOwnerRepository = storeOwnerRepository;
        _requestContextService = requestContextService;
        _mapper = mapper;
    }
    
    public async Task<Result<CreateStoreCommandResponse>> Handle(CreateStoreCommand request, CancellationToken cancellationToken)
    {
        await _unitOfWork.BeginTransactionAsync(cancellationToken);

        try
        {
            var appUserId = _requestContextService.GetCurrentUserId();

            if (appUserId == null)
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                return Result<CreateStoreCommandResponse>.Failure($"Bu kullanici bulunamadi.");
            }

            var storeOwner = await _storeOwnerRepository.GetStoreIdByStoreOwnerIdAsync(appUserId, cancellationToken);
            
            if (storeOwner.Store != null)
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                return Result<CreateStoreCommandResponse>.Failure($"Bu kullanici adina magaza kaydi bulunmaktadir.");
            }
            
            var store = _mapper.Map<Domain.Entities.Store>(request);

            if (store is null)
            {
                return Result<CreateStoreCommandResponse>.Failure("Store map islemi basariz oldu.");
            }
            
            await _storeRepository.AddAsync(store);
            
            storeOwner.Store = store;
            
            _storeOwnerRepository.Update(storeOwner);

            var tags = await _tagRepository.GetTagsByTagIds(request.TagIds,cancellationToken);

            var missingTagIds = request.TagIds.Except(tags.Select(t => t.Id)).ToList();

            if (missingTagIds.Any())
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                return Result<CreateStoreCommandResponse>.Failure($"Aşağıdaki Tag ID'leri bulunamadı: {string.Join(", ", missingTagIds)}");
            }
            
            store.Tags = new List<Domain.Entities.Tag>();
            
            foreach (var tag in tags)
            {
                store.Tags.Add(tag);
            }
            
            await _unitOfWork.SaveChangesAsync(appUserId, cancellationToken);
            await _unitOfWork.CommitTransactionAsync(cancellationToken);
            
            return Result<CreateStoreCommandResponse>.Succeed(new CreateStoreCommandResponse
            {
                StoreId = store.Id
            });
        }
        catch(Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);
            return Result<CreateStoreCommandResponse>.Failure($"Store kaydı başarısız. {ex}");
        }
    }
}