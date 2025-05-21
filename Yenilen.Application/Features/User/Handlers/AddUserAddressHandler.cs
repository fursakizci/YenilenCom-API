using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using TS.Result;
using Yenilen.Application.Features.User.Commands;
using Yenilen.Application.Interfaces;
using Yenilen.Application.Services;
using Yenilen.Application.Services.Common;

namespace Yenilen.Application.Features.User.Handlers;

internal sealed class AddUserAddressHandler : IRequestHandler<AddUserAddressCommand, Result<AddUserAddressCommandResponse>>
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly ICustomerRepository _customerRepository;
    private readonly IAddressRepository _addressRepository;
    private readonly IRequestContextService _requestContextService;
    private readonly IMapper _mapper;

    public AddUserAddressHandler(IUnitOfWork unitOfWork, 
        ICustomerRepository customerRepository,
        IAddressRepository addressRepository, 
        IRequestContextService requestContextService,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _customerRepository = customerRepository;
        _addressRepository = addressRepository;
        _requestContextService = requestContextService;
        _mapper = mapper;
    }
    
    public async Task<Result<AddUserAddressCommandResponse>> Handle(AddUserAddressCommand request, CancellationToken cancellationToken)
    {
        var appUserId = _requestContextService.GetCurrentUserId();

        if (appUserId is null)
        {
            return Result<AddUserAddressCommandResponse>.Failure("Kullanici id bilgisine ulasilamadi.");
        }

        var user = await _customerRepository.GetCustomerByGuid(appUserId);

        if (user == null)
        {
            return Result<AddUserAddressCommandResponse>.Failure("Kullanici bulunamadi.");
        }
        
        var newAddress = _mapper.Map<Domain.Entities.Address>(request);

        await _addressRepository.AddAsync(newAddress);
        
        user.Addresses.Add(newAddress);
        
        await _unitOfWork.SaveChangesAsync(appUserId, cancellationToken);

        var response = new AddUserAddressCommandResponse()
        {
            AddressId = newAddress.Id
        };

        return Result<AddUserAddressCommandResponse>.Succeed(response);
    }
}
