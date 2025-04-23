using MediatR;
using Yenilen.Application.DTOs;
using Yenilen.Application.Features.User.Queries;
using Yenilen.Application.Interfaces;

namespace Yenilen.Application.Features.User.Handlers;

public class GetAllFavouritesByUserIdHandler:IRequestHandler<GetAllFavouritesByUserIdQuery,List<FavouriteDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly IReviewRepository _reviewRepository;
    private readonly IStoreRepository _storeRepository;

    public GetAllFavouritesByUserIdHandler(IUserRepository userRepository, IReviewRepository reviewRepository, IStoreRepository storeRepository)
    {
        _userRepository = userRepository;
        _reviewRepository = reviewRepository;
        _storeRepository = storeRepository;
    }
    
    public async Task<List<FavouriteDto>> Handle(GetAllFavouritesByUserIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetFavouriteByIdAsync(request.UserId);
        
        if (user == null)
            return new List<FavouriteDto>();

        var result = new List<FavouriteDto>();

        foreach (var favorite in user.Favourites)
        {
            var reviewCount = await _reviewRepository.GetReviewCountByStoreId(favorite.StoreId);
            var storeRating = await _reviewRepository.GetStoreRatingByStoreId(favorite.StoreId);
            var storeAddress = await _storeRepository.GetStoreFullAddressById(favorite.StoreId);
            
            result.Add(new FavouriteDto
            {
                Id = favorite.StoreId.ToString(),
                Name = favorite.Store.Name,
                Rating = storeRating,
                ReviewCount = reviewCount,
                FullAddress = storeAddress
            });
        }
        
        return result;
    }
}