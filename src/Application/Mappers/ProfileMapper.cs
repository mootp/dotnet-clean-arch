using Application.Dtos.Response;
using AutoMapper;
using Domain.Entities.Core;

namespace Application.Mappers;

public class ProfileMapper : Profile
{
    public ProfileMapper()
    {
        CreateMap<User, UserResponse>();
    }
}