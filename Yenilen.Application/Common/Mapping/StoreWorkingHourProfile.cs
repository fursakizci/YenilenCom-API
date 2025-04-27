using AutoMapper;
using Yenilen.Application.Features.Store.Commands;
using Yenilen.Domain.Entities;

namespace Yenilen.Application.Common.Mapping;

public class StoreWorkingHourProfile:Profile
{
    public StoreWorkingHourProfile()
    {
        // CreateMap<CreateStoreCommand, Store>()
        //     .ForMember(s => s.StoreName)
    }
    
}