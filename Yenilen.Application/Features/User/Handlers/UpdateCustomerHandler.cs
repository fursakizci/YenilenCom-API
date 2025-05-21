using AutoMapper;
using MediatR;
using TS.Result;
using Yenilen.Application.Features.User.Commands;
using Yenilen.Application.Interfaces;
using Yenilen.Application.Services;
using Yenilen.Application.Services.Common;
using Yenilen.Domain.Entities;

namespace Yenilen.Application.Features.User.Handlers;

internal sealed class UpdateCustomerHandler : IRequestHandler<UpdateCustomerCommand, Result<UpdateCustomerCommandResponse>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICustomerRepository _customerRepository;
    private readonly IRequestContextService _requestContextService;

    public UpdateCustomerHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork,
        ICustomerRepository customerRepository,
        IRequestContextService requestContextService
        )
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _customerRepository = customerRepository;
        _requestContextService = requestContextService;
    }
    
    public async Task<Result<UpdateCustomerCommandResponse>> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var appUserId = _requestContextService.GetCurrentUserId();

        if (appUserId is null)
        {
            return Result<UpdateCustomerCommandResponse>.Failure("Kullanici id bilgisi alinamadi.");
        }

        var customer = await _customerRepository.FirstOrDefaultAsync(s=>s.AppUserId == appUserId, cancellationToken);

        if (customer is null)
        {
            return Result<UpdateCustomerCommandResponse>.Failure("Kullanici bilgisine ulasilamadi.");
        }

        _mapper.Map(request, customer);
        
        _customerRepository.Update(customer);

        try
        {
            await _unitOfWork.SaveChangesAsync(appUserId, cancellationToken);
        }
        catch (Exception ex)
        {
            return Result<UpdateCustomerCommandResponse>.Failure($"Güncelleme sırasında hata oluştu: {ex.Message}");
        }
        
        var response = new UpdateCustomerCommandResponse()
        {
            UserId = customer.Id
        };

        return Result<UpdateCustomerCommandResponse>.Succeed(response);
    }
}