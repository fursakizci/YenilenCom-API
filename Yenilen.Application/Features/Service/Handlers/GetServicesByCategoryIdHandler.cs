using AutoMapper;
using MediatR;
using TS.Result;
using Yenilen.Application.DTOs;
using Yenilen.Application.Features.Service.Queries;
using Yenilen.Application.Interfaces;

namespace Yenilen.Application.Features.Service.Handlers;

public class GetServicesByCategoryIdHandler:IRequestHandler<GetServicesByCategoryIdQuery,Result<List<GetServicesByCategoryIdQueryResponse>>>
{
    private readonly IServiceRepository _serviceRepository;
    private readonly IMapper _mapper;

    public GetServicesByCategoryIdHandler(IServiceRepository serviceRepository, IMapper mapper)
    {
        _serviceRepository = serviceRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<GetServicesByCategoryIdQueryResponse>>> Handle(GetServicesByCategoryIdQuery request, CancellationToken cancellationToken)
    {
        var services = await _serviceRepository.GetServicesByCategoryIdAsync(request.CategoryId);
        
        var servicesDto = _mapper.Map<List<GetServicesByCategoryIdQueryResponse>>(services);
        
        return Result<List<GetServicesByCategoryIdQueryResponse>>.Succeed(servicesDto);
    }
}