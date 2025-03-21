using AutoMapper;
using Yenilen.Application.Features.Users.Commands;
using Yenilen.Domain.Entities;

namespace Yenilen.Application.Common.Mapping;

public class UserMappingProfile:Profile
{
    public UserMappingProfile()
    {
        CreateMap<CreateUserCommand, User>();
    }
}