using AutoMapper;
using MediatR;
using Yenilen.Application.Features.StaffMember.Commands;
using Yenilen.Application.Interfaces;
using Yenilen.Domain.Entities;

namespace Yenilen.Application.Features.StaffMember.Handlers;

internal sealed class CreateStaffMemberHandler:IRequestHandler<CreateStaffMemberCommand,int>
{
    private readonly IStaffRepository _staffRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    
    
    public CreateStaffMemberHandler(IStaffRepository staffRepository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _staffRepository = staffRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<int> Handle(CreateStaffMemberCommand request, CancellationToken cancellationToken)
    {
        var staff = _mapper.Map<Staff>(request);

        await _staffRepository.AddAsync(staff);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return staff.Id;
    }
}