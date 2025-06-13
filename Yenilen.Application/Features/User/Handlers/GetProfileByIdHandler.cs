using MediatR;
using TS.Result;
using Yenilen.Application.DTOs;
using Yenilen.Application.Features.User.Queries;
using Yenilen.Application.Interfaces;
using Yenilen.Application.Services.Common;

namespace Yenilen.Application.Features.User.Handlers;

internal sealed class GetProfileByIdHandler : IRequestHandler<GetProfileByIdQuery, Result<GetProfileByIdQueryResponse>>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IRequestContextService _requestContextService;

    public GetProfileByIdHandler(ICustomerRepository customerRepository,
        IRequestContextService requestContextService)
    {
        _customerRepository = customerRepository;
        _requestContextService = requestContextService;
    }

    public async Task<Result<GetProfileByIdQueryResponse>> Handle(GetProfileByIdQuery request, CancellationToken cancellationToken)
    {
        var appUserId = _requestContextService.GetCurrentUserId();

        if (appUserId == Guid.Empty)
        {
            return Result<GetProfileByIdQueryResponse>.Failure("Kullanıcı bilgisi alınamadı");
        }
        
        var user = await _customerRepository.GetByIdAsync(appUserId, true);

        if (user is null)
        {
            return Result<GetProfileByIdQueryResponse>.Failure("Kullanici bulunamadi");
        };

        var response = new GetProfileByIdQueryResponse
        {
            Id = user.Id.ToString(),
            FirstName = user.FirstName,
            LastName = user.LastName,
            FullName = user.FullName,
            Initials = $"{(user.FirstName?.FirstOrDefault().ToString().ToUpper() ?? "")}{(user.LastName?.FirstOrDefault().ToString().ToUpper() ?? "")}",
            MobileNumber = user.PhoneNumber,
            Email = user.Email,
            DateOfBirth = user.DateOfBirth.ToString("dd/MM/yyyy"),
            Gender = user.Gender?.ToString() ?? "",
            Addresses = user.Addresses != null
                ? user.Addresses.Select(a => new AddressDto
                {
                    Label = a.Label,
                    FullAddress = a.FullAddress,
                    Latitude = a.Latitude,
                    Longitude = a.Longitude,
                    CountryCode = a.CountryCode,
                    Country = a.Country,
                    City = a.City,
                    District = a.District,
                    Region = a.Region,
                    PostCode = a.PostCode
                }).ToList()
                : new List<AddressDto>(),
            Image = user.AvatarUrl != null
                ? new ImageDto
                {
                    Url = user.AvatarUrl.ImageUrl
                }
                : null
        };

        return Result<GetProfileByIdQueryResponse>.Succeed(response);
    }
}