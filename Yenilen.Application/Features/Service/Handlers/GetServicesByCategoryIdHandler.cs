using AutoMapper;
using MediatR;
using Yenilen.Application.DTOs;
using Yenilen.Application.Features.Service.Queries;
using Yenilen.Application.Interfaces;

namespace Yenilen.Application.Features.Service.Handlers;

public class GetServicesByCategoryIdHandler:IRequestHandler<GetServicesByCategoryIdQuery,List<ServiceDto>>
{
    private readonly IServiceRepository _serviceRepository;
    private readonly IMapper _mapper;

    public GetServicesByCategoryIdHandler(IServiceRepository serviceRepository, IMapper mapper)
    {
        _serviceRepository = serviceRepository;
        _mapper = mapper;
    }

    public async Task<List<ServiceDto>> Handle(GetServicesByCategoryIdQuery request, CancellationToken cancellationToken)
    {
        var services = await _serviceRepository.GetServicesByCategoryIdAsync(request.CategoryId);
        
        var servicesDto = _mapper.Map<List<ServiceDto>>(services);
        
        return servicesDto;
    }
}