using AutoMapper;
using Yenilen.Application.Features.User.Commands;
using Yenilen.Application.Features.Users.Commands;
using Yenilen.Domain.Entities;

namespace Yenilen.Application.Common.Mapping;

public class CustomerMappingProfile:Profile
{
    public CustomerMappingProfile()
    {
        CreateMap<CreateUserCommand, Customer>()
            .ForMember(u => u.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(u => u.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(u => u.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(u => u.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(u => u.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth))
            .ForMember(u => u.Gender, opt => opt.MapFrom(src => src.Gender));
        
        CreateMap<UpdateCustomerCommand, Customer>()
            .ForMember(u => u.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(u => u.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(u => u.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(u => u.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(u => u.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth))
            .ForMember(u => u.Gender, opt => opt.MapFrom(src => src.Gender));
    }
}