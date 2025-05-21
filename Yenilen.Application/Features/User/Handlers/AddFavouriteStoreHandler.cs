using AutoMapper;
using MediatR;
using TS.Result;
using Yenilen.Application.Features.User.Commands;
using Yenilen.Application.Interfaces;
using Yenilen.Application.Services;
using Yenilen.Application.Services.Common;
using Yenilen.Domain.Entities;

namespace Yenilen.Application.Features.User.Handlers;

public class AddFavouriteStoreHandler:IRequestHandler<AddFavouriteStoreCommand, Result<AddFavouriteStoreCommandResponse>>
{
    private readonly IStoreRepository _storeRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IRequestContextService _requestContextService;
    private readonly IFavouriteRepository _favouriteRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    
    public AddFavouriteStoreHandler(IStoreRepository storeRepository,
        ICustomerRepository customerRepository,
        IFavouriteRepository favouriteRepository,
        IRequestContextService requestContextService,
        IMapper mapper, IUnitOfWork unitOfWork)
    {
        _customerRepository = customerRepository;
        _storeRepository = storeRepository;
        _favouriteRepository = favouriteRepository;
        _requestContextService = requestContextService;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<AddFavouriteStoreCommandResponse>> Handle(AddFavouriteStoreCommand request, CancellationToken cancellationToken)
    {
        var appUserId = _requestContextService.GetCurrentUserId();

        if (appUserId is null)
        {
            return Result<AddFavouriteStoreCommandResponse>.Failure("Bu kullanici bulunamadi.");
        }
        
        var customer = await _customerRepository.FirstOrDefaultAsync(u=>u.AppUserId == appUserId, cancellationToken);

        if (customer is null)
        {
            return Result<AddFavouriteStoreCommandResponse>.Failure("Bu kullanici bulunamadi.");  
        }
        
        var store = await _storeRepository.GetByIdAsync(request.StoreId);

        if (store == null)
        {
            return Result<AddFavouriteStoreCommandResponse>.Failure("Bu magaza bulunamadi.");
        }
        
        var exists = await _favouriteRepository.ExistsAsync(customer.Id, store.Id, cancellationToken);
        if (exists)
        {
            return Result<AddFavouriteStoreCommandResponse>.Failure("Bu mağaza zaten favorilere eklenmiş.");
        }
        
        var favourite = new Favourite
        {
            CustomerId = customer.Id,
            StoreId = store.Id
        };

        await _favouriteRepository.AddAsync(favourite, cancellationToken);
        
        if (customer.Favourites == null)
        {
            customer.Favourites = new List<Favourite>();
        }
            
        customer.Favourites.Add(favourite);

        await _unitOfWork.SaveChangesAsync(appUserId, cancellationToken);

        var response = new AddFavouriteStoreCommandResponse()
        {
            FavouriteId = favourite.Id
        };

        return Result<AddFavouriteStoreCommandResponse>.Succeed(response);
    }
}