using Application.Dtos.Request;
using Application.Dtos.Response;

namespace Application.Interfaces;
public interface IAuthService
{
    Task<TokenResponse> LoginLocal(AuthLocalRequest model);
    Task<TokenResponse> LoginProvider(AuthProviderRequest model);
    Task<TokenResponse> RefreshToken();
    Task<bool> RevokeSession(Guid id);
    Task<bool> RevokeProvider(Guid id);
    Task<bool> ChangePassword(string oldPass, string newPass, string confPass);
    Task<bool> ResetPassword(Guid id, string newPass);
}