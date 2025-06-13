using MediatR;
using TS.Result;
using Yenilen.Application.Auth.Queries;
using Yenilen.Application.DTOs;
using Yenilen.Application.Interfaces;
using Yenilen.Application.Services.Common;

namespace Yenilen.Application.Auth.Handlers;

internal sealed class MeCustomerHandler : IRequestHandler<MeCustomerQuery,Result<MeCustomerQueryResponse>>
{
    private readonly IRequestContextService _requestContextService;
    private readonly ICustomerRepository _customerRepository;

    public MeCustomerHandler(IRequestContextService requestContextService,
        ICustomerRepository customerRepository)
    {
        _requestContextService = requestContextService;
        _customerRepository = customerRepository;
    }
    
    public async Task<Result<MeCustomerQueryResponse>> Handle(MeCustomerQuery request, CancellationToken cancellationToken)
    {
        var appUserId = _requestContextService.GetCurrentUserId();
        
        if (appUserId == Guid.Empty)
        {
            return Result<MeCustomerQueryResponse>.Failure("Kullanıcı bilgisi alınamadı");
        }

        var user = await _customerRepository.GetCustomerWithAvatarAndAddressesAsync(appUserId, cancellationToken);

        if (user is null)
        {
            return Result<MeCustomerQueryResponse>.Failure("Kullanıcı bulunamadi.");
        }

        var response = new MeCustomerQueryResponse
        {
            Id = user.Id.ToString(),
            FullName = user.FullName,
            Email = user.Email,
            AvatarUrl = user.AvatarUrl?.ImageUrl ?? string.Empty,
            Addresses = user?.Addresses != null 
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
            }).ToList() : new List<AddressDto>()
        };

        return Result<MeCustomerQueryResponse>.Succeed(response);
    }
}

