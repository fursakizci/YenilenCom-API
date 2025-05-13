using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Yenilen.Application.Features.User.Commands;
using Yenilen.Application.Interfaces;
using Yenilen.Application.Services;
using Yenilen.Application.Services.Common;

namespace Yenilen.Application.Features.User.Handlers;

internal sealed class AddUserAddressHandler : IRequestHandler<AddUserAddressCommand, int>
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly IAddressRepository _addressRepository;
    private readonly IRequestContextService _requestContextService;
    private readonly IMapper _mapper;

    public AddUserAddressHandler(IUnitOfWork unitOfWork, 
        IUserRepository userRepository,
        IAddressRepository addressRepository, 
        IHttpContextAccessor httpContextAccessor,
        IRequestContextService requestContextService,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
        _addressRepository = addressRepository;
        _requestContextService = requestContextService;
        _mapper = mapper;
    }
    
    public async Task<int> Handle(AddUserAddressCommand request, CancellationToken cancellationToken)
    {
        var userId = _requestContextService.GetCurrentUserId();

        var user = await _userRepository.GetUserByGuid(userId);
        
        if (user == null)
            throw new Exception("User not found");

        var newAddress = _mapper.Map<Domain.Entities.Address>(request);

        await _addressRepository.AddAsync(newAddress);
        
        user.Addresses.Add(newAddress);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return newAddress.Id;
    }
}
