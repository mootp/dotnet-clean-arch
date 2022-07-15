using Application.Dtos.Request;
using Application.Dtos.Response;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Common;

namespace Application.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UserService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public Task<bool> AddUser(UserRequest model)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteUser(Guid userId)
    {
        throw new NotImplementedException();
    }

    public UserResponse? GetUser(Guid userId)
    {
        throw new NotImplementedException();
    }

    public IQueryable<UserResponse> GetUserList()
    {
        throw new NotImplementedException();
    }
}