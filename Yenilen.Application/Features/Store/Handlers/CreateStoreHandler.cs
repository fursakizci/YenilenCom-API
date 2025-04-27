using AutoMapper;
using MediatR;
using Yenilen.Application.Features.Store.Commands;
using Yenilen.Application.Interfaces;

namespace Yenilen.Application.Features.Store.Handlers;

internal sealed class CreateStoreHandler:IRequestHandler<CreateStoreCommand, int>
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IStoreRepository _storeRepository;
    private readonly ITagRepository _tagRepository;
    private readonly IMapper _mapper;
    
    public CreateStoreHandler(IUnitOfWork unitOfWork, IStoreRepository storeRepository, ITagRepository tagRepository, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _storeRepository = storeRepository;
        _tagRepository = tagRepository;
        _mapper = mapper;
    }
    
    public async Task<int> Handle(CreateStoreCommand request, CancellationToken cancellationToken)
    {

        var store = _mapper.Map<Domain.Entities.Store>(request);
        await _storeRepository.AddAsync(store);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        var tags = _tagRepository.Where(tag => request.TagIds.Contains(tag.Id)).ToList();
        foreach (var tag in tags)
        {
            store.Tags.Add(tag);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return store.Id;
    }
}