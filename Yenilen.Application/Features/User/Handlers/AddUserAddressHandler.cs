using AutoMapper;
using MediatR;
using Yenilen.Application.Features.Users.Commands;
using Yenilen.Application.Interfaces;

namespace Yenilen.Application.Features.User.Handlers;

internal sealed class AddUserAddressHandler : IRequestHandler<AddUserAddressCommand, int>
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly IAddressRepository _addressRepository;
    private readonly IMapper _mapper;

    public AddUserAddressHandler(IUnitOfWork unitOfWork, IUserRepository userRepository,IAddressRepository addressRepository, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
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

        //newAddress.UserId = user.Id;
        // newAddress.Store = null;
        // newAddress.StoreId = null;
        await _addressRepository.AddAsync(newAddress);
        
        user.Addresses.Add(newAddress);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return newAddress.Id;
    }
}
