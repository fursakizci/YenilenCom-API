using AutoMapper;
using MediatR;
using Yenilen.Application.Features.Booking.Commands;
using Yenilen.Application.Interfaces;
using Yenilen.Application.Services;
using Yenilen.Domain.Entities;

namespace Yenilen.Application.Features.Booking.Handlers;

public class CreateAppointmetnHandler:IRequestHandler<CreateAppointmentCommand,int>
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IServiceRepository _serviceRepository;
    private readonly IStoreRepository _storeRepository;
    private readonly IStaffRepository _staffRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    public CreateAppointmetnHandler(IAppointmentRepository appointmentRepository,
        IServiceRepository serviceRepository,
        IStoreRepository storeRepository,
        IStaffRepository staffRepository,
        IMapper mapper, 
        IUnitOfWork unitOfWork)
    {
        _appointmentRepository = appointmentRepository;
        _storeRepository = storeRepository;
        _staffRepository = staffRepository;
        _serviceRepository = serviceRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<int> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
    {

        var storeExist = await _storeRepository.AnyAsync(s => s.Id == request.StoreId);
        
        if (!storeExist)
            throw new InvalidOperationException("Isletme bulunamadi.");

        var staffExist = await _staffRepository.AnyAsync(s => s.Id == request.StaffId);

        if (!staffExist)
            throw new InvalidOperationException("Calisan bulunamadi");

        var allServicesExist = await _serviceRepository.AllServicesExistAsync(request.ServicesId);
        
        if (!allServicesExist)
            throw new InvalidOperationException("Girdiginiz servis bilgisi bulunamadi.");

        var services = await _serviceRepository.GetByIdsAsync(request.ServicesId, cancellationToken);
        var appointment =  _mapper.Map<Appointment>(request);
        appointment.StartTime = DateTime.SpecifyKind(request.AppointmentDate, DateTimeKind.Utc);
        appointment.Services = services.ToList();
        
        await _appointmentRepository.AddAsync(appointment);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return appointment.Id;
    }
}