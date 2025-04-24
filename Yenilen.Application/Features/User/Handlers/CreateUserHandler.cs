using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Yenilen.Application.Features.Users.Commands;
using Yenilen.Application.Interfaces;
using Yenilen.Domain.Entities;

namespace Yenilen.Application.Features.User.Handlers;

internal class CreateUserHandler:IRequestHandler<CreateUserCommand, int>
{

    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserHandler(IUnitOfWork unitOfWork,IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var isUserExists = await _userRepository.IsExistsAsync(request.PhoneNumber, request.Email);
        
        if(isUserExists)
            throw new InvalidOperationException("Girdiğiniz kullanıcıya ait telefon numarası ya da email sistemde kayıtlıdır.");
        
        var user = _mapper.Map<Domain.Entities.User>(request);

        await _userRepository.AddAsync(user);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return user.Id;
    }
}

