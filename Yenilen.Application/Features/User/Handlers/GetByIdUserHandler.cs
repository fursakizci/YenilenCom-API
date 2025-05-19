using MediatR;
using TS.Result;
using Yenilen.Application.DTOs;
using Yenilen.Application.Features.User.Queries;
using Yenilen.Application.Interfaces;

namespace Yenilen.Application.Features.User.Handlers;

internal sealed class GetByIdUserHandler: IRequestHandler<GetByIdUserQuery,Result<GetByIdUserQueryResponse>>
{
    private readonly IUserRepository _userRepository;

    public GetByIdUserHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task<Result<GetByIdUserQueryResponse>> Handle(GetByIdUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId);

        if (user == null) return null;

        var response = new GetByIdUserQueryResponse()
        {
            Name = user.FirstName,
            Surname = user.LastName,
            Phone = user.PhoneNumber,
            Email = user.Email,
            DateOfBirth = user.DateOfBirth,
            Gender = user.Gender.ToString()
        };

        return Result<GetByIdUserQueryResponse>.Succeed(response);
    }
}