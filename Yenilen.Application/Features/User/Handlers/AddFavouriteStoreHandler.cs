using AutoMapper;
using MediatR;
using TS.Result;
using Yenilen.Application.Features.User.Commands;
using Yenilen.Application.Interfaces;
using Yenilen.Application.Services;
using Yenilen.Domain.Entities;

namespace Yenilen.Application.Features.User.Handlers;

public class AddFavouriteStoreHandler:IRequestHandler<AddFavouriteStoreCommand, Result<AddFavouriteStoreCommandResponse>>
{
    private readonly IStoreRepository _storeRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    
    public AddFavouriteStoreHandler(IStoreRepository storeRepository,IUserRepository userRepository,
        IMapper mapper, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _storeRepository = storeRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<AddFavouriteStoreCommandResponse>> Handle(AddFavouriteStoreCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId);
        var store = await _storeRepository.GetByIdAsync(request.StoreId);
        
        if(user == null || store == null)
            throw new InvalidOperationException("Kullanici veya isletme bulunamadi.");

        var favourite = new Favourite
        {
            User = user,
            UserId = user.Id,
            Store = store,
            StoreId = store.Id
        };
        
        if (user.Favourites == null)
        {
            user.Favourites = new List<Favourite>();
        }
            
        user.Favourites.Add(favourite);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var response = new AddFavouriteStoreCommandResponse()
        {
            FavouriteId = favourite.Id
        };

        return Result<AddFavouriteStoreCommandResponse>.Succeed(response);
    }
}