using MediatR;
using Yenilen.Application.DTOs;
using Yenilen.Application.Features.Profile.Queries;
using Yenilen.Application.Interfaces;

namespace Yenilen.Application.Features.Profile.Handlers;

public class GetProfileByIdHandler : IRequestHandler<GetProfileByIdQuery, ProfileDto>
{
    private readonly IUserRepository _userRepository;

    public GetProfileByIdHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ProfileDto> Handle(GetProfileByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId, true);
        
        if (user == null) return null;

        var profileDto = new ProfileDto
        {
            Id = user.Id.ToString(),
            Name = user.FirstName,
            Surname = user.LastName,
            Phone = user.PhoneNumber,
            Email = user.Email,
            DateOfBirth = user.DateOfBirth.ToString("dd/MM/yyyy"),
            Sex = user.Gender.ToString(),
            Address = user.Addresses != null
                ? user.Addresses.Select(a => new AddressDto
                {
                    Id = a.Id,
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

        return profileDto;
    }
}