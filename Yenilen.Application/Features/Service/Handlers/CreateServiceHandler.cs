using AutoMapper;
using MediatR;
using TS.Result;
using Yenilen.Application.Features.Service.Commands;
using Yenilen.Application.Interfaces;
using Yenilen.Application.Services;

namespace Yenilen.Application.Features.Service.Handlers;

internal sealed class CreateServiceHandler : IRequestHandler<CreateServiceCommand,Result<CreateServiceCommandResponse>>
{

    private readonly IServiceRepository _serviceRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
        
    public CreateServiceHandler(IServiceRepository serviceRepository,
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _serviceRepository = serviceRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<CreateServiceCommandResponse>> Handle(CreateServiceCommand request, CancellationToken cancellationToken)
    {
        var isExistService = await _serviceRepository.AnyAsync(s => s.Name == request.Name);

        if (isExistService)
        {
            throw new InvalidOperationException("Girdiginiz Servis adina ait servisiniz bulunmaktadir");
        }

        var service = _mapper.Map<Domain.Entities.Service>(request);

        await _serviceRepository.AddAsync(service);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var result = new CreateServiceCommandResponse()
        {
            ServiceId = service.Id
        };

        return Result<CreateServiceCommandResponse>.Succeed(result);
    }
}