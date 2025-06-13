using MediatR;
using TS.Result;
using Yenilen.Application.DTOs;
using Yenilen.Application.Features.Store.Queries;
using Yenilen.Application.Interfaces;
using Yenilen.Domain.Entities;

namespace Yenilen.Application.Features.Store.Handlers;

internal sealed class GetStoresFromSearchBarHandler:IRequestHandler<GetStoresFromSearchBarQuery,Result<IQueryable<GetStoresFromSearchBarQueryResponse>>>
{
    private readonly IStoreRepository _storeRepository;

    public GetStoresFromSearchBarHandler(IStoreRepository storeRepository)
    {
        _storeRepository = storeRepository;
    }
    
    public Task<Result<IQueryable<GetStoresFromSearchBarQueryResponse>>> Handle(GetStoresFromSearchBarQuery request, CancellationToken cancellationToken)
    {
        var storesInArea = _storeRepository.GetAll().Where(s =>
                s.Address.Latitude >= request.MinLatitude &&
                s.Address.Latitude <= request.MaxLatitude &&
                s.Address.Longitude >= request.MinLongitude &&
                s.Address.Longitude <= request.MaxLongitude 
                //&& s.Tags.Any(t => t.Id == request.TagId)
                )
            .Select(s => new GetStoresFromSearchBarQueryResponse
            {
                StoreId = s.Id,
                Name = s.StoreName,
                Address = new AddressDto
                {
                    FullAddress = s.Address.FullAddress,
                    City = s.Address.City,
                    Latitude = s.Address.Latitude,
                    Longitude = s.Address.Longitude
                },
                ImageUrls = s.Images.Select(i => i.ImageUrl).ToList(),
                Services = s.Categories.SelectMany(c => c.Services.Select(s => new ServiceDto
                {
                    ServiceId = s.Id.ToString(),
                    Name = s.Name,
                    Price = s.Price,
                    Duration = s.Duration.TotalMinutes.ToString()
                })).ToList()
            });

        return Task.FromResult(Result<IQueryable<GetStoresFromSearchBarQueryResponse>>.Succeed(storesInArea));
    }
}