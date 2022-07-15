
using Application.Dtos.Request;
using Application.Dtos.Response;

namespace Application.Interfaces;
public interface IUserService
{
    IQueryable<UserResponse> GetUserList();
    UserResponse? GetUser(Guid userId);
    Task<bool> AddUser(UserRequest model);
    Task<bool> DeleteUser(Guid userId);
}