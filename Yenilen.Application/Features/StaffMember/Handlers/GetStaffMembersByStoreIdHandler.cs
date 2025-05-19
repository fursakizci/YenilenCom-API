using AutoMapper;
using MediatR;
using TS.Result;
using Yenilen.Application.DTOs;
using Yenilen.Application.Features.StaffMember.Queries;
using Yenilen.Application.Interfaces;

namespace Yenilen.Application.Features.StaffMember.Handlers;

public class GetStaffMembersByStoreIdHandler:IRequestHandler<GetStaffMembersByStoryIdQuery,Result<List<GetStaffMembersByStoryIdQueryResponse>>>
{
    private readonly IStaffRepository _staffRepository;
    private readonly IMapper _mapper;

    public GetStaffMembersByStoreIdHandler(IStaffRepository staffRepository, IMapper mapper)
    {
        _staffRepository = staffRepository;
        _mapper = mapper;
    }
    public async Task<Result<List<GetStaffMembersByStoryIdQueryResponse>>> Handle(GetStaffMembersByStoryIdQuery request, CancellationToken cancellationToken)
    {
        var staffMembers = await _staffRepository.GetStaffMembersByStoreIdAsync(request.StoreId);

        if (staffMembers == null)
            return Result<List<GetStaffMembersByStoryIdQueryResponse>>.Failure("Kullanici Bulunamadi");

        var staffMembersDto = _mapper.Map<List<GetStaffMembersByStoryIdQueryResponse>>(staffMembers);
        
        return Result<List<GetStaffMembersByStoryIdQueryResponse>>.Succeed(staffMembersDto);
    }
}