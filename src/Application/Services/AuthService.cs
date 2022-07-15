using Application.Dtos.Request;
using Application.Dtos.Response;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities.Auth;
using Domain.Enums;
using Domain.Helpers;
using Domain.Interfaces.Common;
using Google.Apis.Auth;
using BC = BCrypt.Net.BCrypt;

namespace Application.Services;
public class AuthService : IAuthService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly AuthSettings _authSettings;
    private readonly IJwtTokenGenerator _jwtToken;

    public AuthService(IUnitOfWork unitOfWork, IMapper mapper, AuthSettings authSettings, IJwtTokenGenerator jwtToken)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _authSettings = authSettings;
        _jwtToken = jwtToken;
    }

    public Task<bool> ChangePassword(string oldPass, string newPass, string confPass)
    {
        throw new NotImplementedException();
    }

    public async Task<TokenResponse> LoginLocal(AuthLocalRequest model)
    {
        var authRepo = _unitOfWork.GetRepository<AuthLocal>();
        var sessionRepo = _unitOfWork.GetRepository<AuthSession>();
        var user = authRepo.FirstOrDefault(x => x.Username == model.Username);
        if (user != null)
        {
            var verify = BC.Verify(model.Password, user.PasswordHash);
            if (verify)
            {
                var userId = user.Id;
                var accessToken = _jwtToken.GenerateToken(userId);
                var refreshToken = _jwtToken.GenerateRefreshToken();
                var session = new AuthSession();
                session.SetSession(userId, refreshToken);
                sessionRepo.Add(session);
                await _unitOfWork.SaveChangesAsync();

                return new TokenResponse()
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken,
                };
            }
            else
            {
                throw new Exception("Password is wrong.");
            }
        }
        else
        {
            throw new Exception("Username not found.");
        }
    }

    public async Task<TokenResponse> LoginProvider(AuthProviderRequest model)
    {
        var authRepo = _unitOfWork.GetRepository<AuthProvider>();
        var sessionRepo = _unitOfWork.GetRepository<AuthSession>();
        if (model.Provider == ProviderAuth.Google)
        {
            GoogleJsonWebSignature.ValidationSettings settings = new GoogleJsonWebSignature.ValidationSettings();
            // Change this to your google client ID
            settings.Audience = new List<string>() { _authSettings.Google };

            GoogleJsonWebSignature.Payload payload = GoogleJsonWebSignature.ValidateAsync(model.Token, settings).Result;
            var auth = authRepo.FirstOrDefault(x => x.Provider == ProviderAuth.Google && x.Token == payload.JwtId);
            if (auth != null)
            {
                var userId = auth.UserId;
                var accessToken = _jwtToken.GenerateToken(userId);
                var refreshToken = _jwtToken.GenerateRefreshToken();
                var session = new AuthSession();
                session.SetSession(userId, refreshToken);
                sessionRepo.Add(session);
                await _unitOfWork.SaveChangesAsync();

                return new TokenResponse()
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken,
                };
            }
            else
            {
                var provider = new AuthProvider();
                // provider.SetProvider()

                return new TokenResponse();
            }
        }
        else
        {
            throw new Exception("Not have provider.");
        }
    }

    public Task<TokenResponse> RefreshToken()
    {
        throw new NotImplementedException();
    }

    public Task<bool> ResetPassword(Guid id, string newPass)
    {
        throw new NotImplementedException();
    }

    public Task<bool> RevokeProvider(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> RevokeSession(Guid id)
    {
        throw new NotImplementedException();
    }
}