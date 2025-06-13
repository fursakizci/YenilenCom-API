using AutoMapper;
using MediatR;
using TS.Result;
using Yenilen.Application.DTOs;
using Yenilen.Application.Features.Store.Queries;
using Yenilen.Application.Interfaces;

namespace Yenilen.Application.Features.Store.Handlers;

internal sealed class GetStoreByIdHandler:IRequestHandler<GetStoreByIdQuery,Result<GetStoreByIdQueryResponse>>
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
    
    public async Task<Result<GetStoreByIdQueryResponse>> Handle(GetStoreByIdQuery request, CancellationToken cancellationToken)
    {
        if (!int.TryParse(request.StoreId, out int storeId))
        {
            return Result<GetStoreByIdQueryResponse>.Failure("Dogru store id degeri girin."); 
        }
            
        var store = await _storeRepository.GetStoreWithDetailsAsync(storeId);
        var storeRating = await _reviewRepository.GetStoreRatingByStoreId(storeId);
        // var resume = _mapper.Map<StoreIndividualDto>(store);
        // resume.Rating = storeRating;
        var response = new GetStoreByIdQueryResponse
        {
            Id = store.Id,
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
            StoreWorkingHours = store.StoreWorkingHours.Select(wh => new StoreWorkingHourDto
            {
                OpeningTime = wh.OpeningTime,
                ClosingTime = wh.ClosingTime,
                DayOfWeek = wh.DayOfWeek,
                IsClosed = wh.IsClosed
            }).ToList(),
            Reviews = store.Reviews.Select(r => new ReviewDto
            {
                Id = r.Id,
                Text = r.Text,
                CreatedAt = r.CreatedAt
            }).ToList(),
            Categories = store.Categories.Select(c => new CategoryDto
            {
                Id = c.Id.ToString(),
                Name = c.Name,
                Services = c.Services.Select(se => new ServiceDto
                {
                    CategoryId = se.CategoryId.ToString(),
                    ServiceId = se.Id.ToString(),
                    Name = se.Name,
                    Price = se.Price,
                    Duration = se.Duration.TotalMinutes.ToString()
                }).ToList()
            }).ToList(),
            StaffMembers = store.StaffMembers.Select(s => new StaffDto
            {
                Id = s.Id,
                FirstName = s.FirstName,
                LastName = s.LastName,
                ImageUrl = s.Image.ImageUrl
            }).ToList(),
            About = store.About,
            Rating = storeRating
        };
        
         return Result<GetStoreByIdQueryResponse>.Succeed(response);
    }
}