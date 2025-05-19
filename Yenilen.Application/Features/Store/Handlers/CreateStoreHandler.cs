using AutoMapper;
using MediatR;
using TS.Result;
using Yenilen.Application.Features.Store.Commands;
using Yenilen.Application.Interfaces;
using Yenilen.Application.Services;

namespace Yenilen.Application.Features.Store.Handlers;

internal sealed class CreateStoreHandler:IRequestHandler<CreateStoreCommand, Result<CrateStoreCommandResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IStoreRepository _storeRepository;
    private readonly ITagRepository _tagRepository;
    private readonly IMapper _mapper;
    
    public CreateStoreHandler(IUnitOfWork unitOfWork, 
        IStoreRepository storeRepository, 
        ITagRepository tagRepository, 
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _storeRepository = storeRepository;
        _tagRepository = tagRepository;
        _mapper = mapper;
    }
    
    public async Task<Result<CrateStoreCommandResponse>> Handle(CreateStoreCommand request, CancellationToken cancellationToken)
    {
        
        await _unitOfWork.BeginTransactionAsync(cancellationToken);

        try
        {
            var store = _mapper.Map<Domain.Entities.Store>(request);

            if (store is null)
            {
                return Result<CrateStoreCommandResponse>.Failure("Store map islemi basariz oldu.");
            }
            
            await _storeRepository.AddAsync(store);

            var tags = await _tagRepository.GetTagsByTagIds(request.TagIds,cancellationToken);

            var missingTagIds = request.TagIds.Except(tags.Select(t => t.Id)).ToList();

            if (missingTagIds.Any())
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                return Result<CrateStoreCommandResponse>.Failure($"Aşağıdaki Tag ID'leri bulunamadı: {string.Join(", ", missingTagIds)}");
            }
            
            store.Tags = new List<Domain.Entities.Tag>();
            foreach (var tag in tags)
            {
                store.Tags.Add(tag);
            }
            
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitTransactionAsync(cancellationToken);
            
            return Result<CrateStoreCommandResponse>.Succeed(new CrateStoreCommandResponse
            {
                StoreId = store.Id
            });
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);
            return Result<CrateStoreCommandResponse>.Failure("Store kaydı başarısız.");
        }
    }
}