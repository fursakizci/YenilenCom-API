using MediatR;
using Yenilen.Application.DTOs;
using Yenilen.Application.Features.Users.Queries;
using Yenilen.Application.Interfaces;

namespace Yenilen.Application.Features.Users.Handlers;

public class GetByIdUserHandler: IRequestHandler<GetByIdUserQuery,UserDto>
{
    private readonly IUserRepository _userRepository;

    public GetByIdUserHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task<UserDto> Handle(GetByIdUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId);

        if (user == null) return null;

        return new UserDto
        {
            Name = user.Name,
            Surname = user.Surname,
            PhoneNumber = user.PhoneNumber,
            Email = user.Email,
            DateOfBirth = user.DateOfBirth,
            Sex = user.Sex
        };
    }
}