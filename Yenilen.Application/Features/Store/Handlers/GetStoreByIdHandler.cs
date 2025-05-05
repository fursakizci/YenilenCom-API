using AutoMapper;
using MediatR;
using Yenilen.Application.DTOs;
using Yenilen.Application.Features.Store.Queries;
using Yenilen.Application.Interfaces;

namespace Yenilen.Application.Features.Store.Handlers;

internal sealed class GetStoreByIdHandler:IRequestHandler<GetStoreByIdQuery,StoreIndividualDto>
{
    private readonly IStoreRepository _storeRepository;
    private readonly IReviewRepository _reviewRepository;
    
    public GetStoreByIdHandler(IStoreRepository storeRepository,
        IReviewRepository reviewRepository 
    )
    {
        _storeRepository = storeRepository;
        _reviewRepository = reviewRepository;
    }
    
    public async Task<StoreIndividualDto> Handle(GetStoreByIdQuery request, CancellationToken cancellationToken)
    {
        var store = await _storeRepository.GetStoreWithDetailsAsync(request.StoreId);
        var storeRating = await _reviewRepository.GetStoreRatingByStoreId(request.StoreId);
        // var resume = _mapper.Map<StoreIndividualDto>(store);
        // resume.Rating = storeRating;
        var resume = new StoreIndividualDto
        {
            Name = store.StoreName,
            Address = new AddressDto
            {
                Label = store.Address.Label,
                FullAddress = store.Address.FullAddress,
                District = store.Address.District,
                City = store.Address.City,
                Region = store.Address.Region,
                Country = store.Address.Country,
                CountryCode = store.Address.CountryCode,
                PostCode = store.Address.PostCode,
                Latitude = store.Address.Latitude,
                Longitude = store.Address.Longitude
            },
            Images = store.Images.Select(img => new ImageDto
            {
                Url = img.ImageUrl
            }).ToList(),
            StoreWorkingHours = store.WorkingHours.Select(wh => new StoreWorkingHourDto
            {
                OpeningTime = wh.OpeningTime,
                ClosingTime = wh.ClosingTime,
                DayOfWeek = wh.DayOfWeek,
                IsClosed = wh.IsClosed
            }).ToList(),
            Reviews = store.Reviews.Select(r => new ReviewDto
            {
                // TODO: Populate properties accordingly
            }).ToList(),
            Categories = store.Categories.Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                Services = c.Services.Select(s => new ServiceDto
                {
                    // TODO: Populate service properties accordingly
                }).ToList()
            }).ToList(),
            StaffMembers = store.StaffMembers.Select(s => new StaffDto
            {
                // TODO: Populate staff properties accordingly
            }).ToList(),
            About = store.About,
            Rating = storeRating
        };
        return resume;
    }
}