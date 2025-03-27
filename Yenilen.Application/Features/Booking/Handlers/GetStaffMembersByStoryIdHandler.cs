using AutoMapper;
using MediatR;
using Yenilen.Application.DTOs;
using Yenilen.Application.Features.Booking.Queries;
using Yenilen.Application.Interfaces;

namespace Yenilen.Application.Features.Booking.Handlers;

public class GetStaffMembersByStoryIdHandler:IRequestHandler<GetStaffMembersByStoryIdQuery,List<StaffDto>>
{
    private readonly IStaffRepository _staffRepository;
    private readonly IMapper _mapper;

    public GetStaffMembersByStoryIdHandler(IStaffRepository staffRepository, IMapper mapper)
    {
        _staffRepository = staffRepository;
        _mapper = mapper;
    }
    public async Task<List<StaffDto>> Handle(GetStaffMembersByStoryIdQuery request, CancellationToken cancellationToken)
    {
        var staffMembers = await _staffRepository.GetStaffMembersByStoreId(request.StoreId);

        if (staffMembers == null)
            return new List<StaffDto>();

        var staffMembersDto = _mapper.Map<List<StaffDto>>(staffMembers);
        
        return staffMembersDto;
    }
}