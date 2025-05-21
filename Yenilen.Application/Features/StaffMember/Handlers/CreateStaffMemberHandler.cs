using AutoMapper;
using MediatR;
using TS.Result;
using Yenilen.Application.Features.StaffMember.Commands;
using Yenilen.Application.Interfaces;
using Yenilen.Application.Services;
using Yenilen.Application.Services.Common;
using Yenilen.Domain.Entities;

namespace Yenilen.Application.Features.StaffMember.Handlers;

internal sealed class CreateStaffMemberHandler:IRequestHandler<CreateStaffMemberCommand,Result<CreateStaffMemberCommandResponse>>
{
    private readonly IStaffRepository _staffRepository;
    private readonly IRequestContextService _requestContextService;
    private readonly IStoreRepository _storeRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    
    public CreateStaffMemberHandler(IStaffRepository staffRepository, 
        IRequestContextService requestContextService,
        IStoreRepository storeRepository,
        IMapper mapper, IUnitOfWork unitOfWork)
    {
        _staffRepository = staffRepository;
        _requestContextService = requestContextService;
        _storeRepository = storeRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<CreateStaffMemberCommandResponse>> Handle(CreateStaffMemberCommand request, CancellationToken cancellationToken)
    {
        var appUser = _requestContextService.GetCurrentUserId();

        if (appUser is null)
        {
            return Result<CreateStaffMemberCommandResponse>.Failure("Magaza sahibi bulunamadi");
        }

        await _unitOfWork.BeginTransactionAsync(cancellationToken);

        try
        {
            var store = await _storeRepository.GetStoreByUserIdAsync(appUser);

            if (store is null)
            {
                return Result<CreateStaffMemberCommandResponse>.Failure("Magaza bulunamadi."); 
            }
        
            var staff = _mapper.Map<Staff>(request);
        
            await _staffRepository.AddAsync(staff);
        
            store!.StaffMembers.Add(staff);
        

            await _unitOfWork.SaveChangesAsync(appUser,cancellationToken);
            await _unitOfWork.CommitTransactionAsync(cancellationToken);
            
            var result = new CreateStaffMemberCommandResponse()
            {
                StaffId = staff.Id
            };

            return Result<CreateStaffMemberCommandResponse>.Succeed(result);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);
            return Result<CreateStaffMemberCommandResponse>.Failure("Personel eklenirken bir hata oluştu: " + ex.Message);
        }
    }
}