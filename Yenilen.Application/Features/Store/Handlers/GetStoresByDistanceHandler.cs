using MediatR;
using TS.Result;
using Yenilen.Application.DTOs;
using Yenilen.Application.Features.Store.Queries;
using Yenilen.Application.Interfaces;

namespace Yenilen.Application.Features.Store.Handlers;

internal sealed class GetStoresByDistanceHandler:IRequestHandler<GetStoresByDistanceQuery,Result<List<GetStoresByDistanceQueryResponse>>>
{
    private readonly IStoreRepository _storeRepository;

    public GetStoresByDistanceHandler(IStoreRepository storeRepository)
    {
        _storeRepository = storeRepository;
    }
    
    public Task<Result<List<GetStoresByDistanceQueryResponse>>> Handle(GetStoresByDistanceQuery request, CancellationToken cancellationToken)
    {
        var query = _storeRepository.Where(s => s.Address != null &&
                                                s.Address.Latitude >= request.MinLatitude &&
                                                s.Address.Latitude <= request.MaxLatitude &&
                                                s.Address.Longitude >= request.MinLongitude &&
                                                s.Address.Longitude <= request.MaxLongitude);

        if (request.TagId.HasValue)
        {
            query = query.Where(s => s.Tags.Any(t => t.Id == request.TagId.Value));
        }

        var storeList = query
            .Select(s => new GetStoresByDistanceQueryResponse
            {
                StoreId = s.Id,
                Name = s.StoreName,
                Rating = 4,
                //CountOfReview = s.Reviews.Count(),
                ImageUrls = s.Images.Select(img => img.ImageUrl).ToList(),
                Address = new AddressDto
                {
                    FullAddress = s.Address.FullAddress,
                    District = s.Address.District,
                    City = s.Address.City,
                    Region = s.Address.Region,
                    Country = s.Address.Country,
                    CountryCode = s.Address.CountryCode,
                    PostCode = s.Address.PostCode,
                    Latitude = s.Address.Latitude,
                    Longitude = s.Address.Longitude
                },
                Services = s.Categories
                    .SelectMany(c => c.Services)
                    .Select(s => new ServiceDto
                    {
                        ServiceId = s.Id,
                        Name = s.Name,
                        Price = s.Price,
                        Duration = s.Duration.ToString()
                    }).ToList()
            }).ToList();
        
        return Task.FromResult(Result<List<GetStoresByDistanceQueryResponse>>.Succeed(storeList));
    }
}