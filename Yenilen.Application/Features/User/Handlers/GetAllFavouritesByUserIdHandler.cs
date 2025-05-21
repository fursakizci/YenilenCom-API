using MediatR;
using TS.Result;
using Yenilen.Application.DTOs;
using Yenilen.Application.Features.User.Queries;
using Yenilen.Application.Interfaces;

namespace Yenilen.Application.Features.User.Handlers;

public class GetAllFavouritesByUserIdHandler:IRequestHandler<GetAllFavouritesByUserIdQuery,Result<List<GetAllFavouritesByUserIdQueryResponse>>>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IReviewRepository _reviewRepository;
    private readonly IStoreRepository _storeRepository;

    public GetAllFavouritesByUserIdHandler(ICustomerRepository customerRepository, IReviewRepository reviewRepository, IStoreRepository storeRepository)
    {
        _customerRepository = customerRepository;
        _reviewRepository = reviewRepository;
        _storeRepository = storeRepository;
    }
    
    public async Task<Result<List<GetAllFavouritesByUserIdQueryResponse>>> Handle(GetAllFavouritesByUserIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _customerRepository.GetFavouriteByIdAsync(request.UserId);
        
        if (user == null)
            return new List<GetAllFavouritesByUserIdQueryResponse>();

        var result = new List<GetAllFavouritesByUserIdQueryResponse>();

        foreach (var favorite in user.Favourites)
        {
            var reviewCount = await _reviewRepository.GetReviewCountByStoreId(favorite.StoreId);
            var storeRating = await _reviewRepository.GetStoreRatingByStoreId(favorite.StoreId);
            var storeAddress = await _storeRepository.GetStoreFullAddressById(favorite.StoreId);
            
            result.Add(new GetAllFavouritesByUserIdQueryResponse
            {
                Id = favorite.StoreId.ToString(),
                Name = favorite.Store.StoreName,
                Rating = storeRating,
                ReviewCount = reviewCount,
                FullAddress = storeAddress
            });
        }
        
        return Result<List<GetAllFavouritesByUserIdQueryResponse>>.Succeed(result);
    }
}