using AutoMapper;
using MediatR;
using Yenilen.Application.Features.Users.Commands;
using Yenilen.Application.Interfaces;

namespace Yenilen.Application.Features.User.Handlers;

public class AddUserAddressHandler : IRequestHandler<AddUserAddressCommand, int>
{

    private readonly IUserRepository _userRepository;
    private readonly IAddressRepository _addressRepository;
    private readonly IMapper _mapper;

    public AddUserAddressHandler(IUserRepository userRepository,IAddressRepository addressRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _addressRepository = addressRepository;
        _mapper = mapper;
    }
    
    public async Task<int> Handle(AddUserAddressCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId);
        if (user == null)
            throw new Exception("User not found");

        var newAddress = _mapper.Map<Domain.Entities.Address>(request);

        newAddress.UserId = user.Id;

        await _addressRepository.AddAsync(newAddress);
        
        user.Addresses.Add(newAddress);

        return newAddress.Id;
    }
}
