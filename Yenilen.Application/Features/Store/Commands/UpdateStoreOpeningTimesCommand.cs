using MediatR;
using TS.Result;
using Yenilen.Application.DTOs;

namespace Yenilen.Application.Features.Store.Commands;

public sealed class UpdateStoreOpeningTimesCommand:IRequest<Result<UpdateStoreOpeningTimesCommandResponse>>
{
    public int StoreId { get; set; }
    public List<StoreWorkingHourDto> StoreWorkingHours { get; set; } = new();
}

public sealed class UpdateStoreOpeningTimesCommandResponse
{
    public List<StoreWorkingHourDto> StoreWorkingHours { get; set; } = new();
}

