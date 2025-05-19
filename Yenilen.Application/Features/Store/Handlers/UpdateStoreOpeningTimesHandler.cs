using AutoMapper;
using MediatR;
using TS.Result;
using Yenilen.Application.DTOs;
using Yenilen.Application.Features.Store.Commands;
using Yenilen.Application.Interfaces;
using Yenilen.Application.Services;
using Yenilen.Domain.Entities;

namespace Yenilen.Application.Features.Store.Handlers;

internal sealed class UpdateStoreOpeningTimesHandler:IRequestHandler<UpdateStoreOpeningTimesCommand, Result<UpdateStoreOpeningTimesCommandResponse>>
{
    private readonly IStoreRepository _storeRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateStoreOpeningTimesHandler(IStoreRepository storeRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _storeRepository = storeRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<Result<UpdateStoreOpeningTimesCommandResponse>> Handle(UpdateStoreOpeningTimesCommand request, CancellationToken cancellationToken)
    {
        var store = await _storeRepository.GetStoreWithDetailsAsync(request.StoreId);

        var storeWorkingHours = _mapper.Map<List<StoreWorkingHour>>(request.StoreWorkingHours);
        store.WorkingHours = storeWorkingHours;
        _storeRepository.Update(store);
        await _unitOfWork.SaveChangesAsync(cancellationToken);


        var storeWorkingHoursResponse = _mapper.Map<List<StoreWorkingHourDto>>(store.WorkingHours);
        var response = new UpdateStoreOpeningTimesCommandResponse()
        {
            StoreWorkingHours = storeWorkingHoursResponse
        };
        
        return Result<UpdateStoreOpeningTimesCommandResponse>.Succeed(response);
    }
}