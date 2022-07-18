using Application.Dtos.Request;
using Application.Dtos.Response;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities.Auth;
using Domain.Entities.Core;
using Domain.Enums;
using Domain.Helpers;
using Domain.Interfaces.Common;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using BC = BCrypt.Net.BCrypt;

namespace Application.Services;
public class AuthService : IAuthService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly AuthSettings _authSettings;
    private readonly IJwtTokenGenerator _jwtToken;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthService(IUnitOfWork unitOfWork, IMapper mapper, AuthSettings authSettings, IJwtTokenGenerator jwtToken)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _authSettings = authSettings;
        _jwtToken = jwtToken;
    }

    public async Task<TokenResponse> LoginLocal(AuthLocalRequest model)
    {
        try
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
        catch (Exception ex)
        {
            throw new AppException(ex.Message);
        }
    }

    public async Task<TokenResponse> LoginProvider(AuthProviderRequest model)
    {
        try
        {
            _unitOfWork.BeginTransaction();
            var token = new TokenResponse();

            if (model.Provider == ProviderAuth.Google)
            {
                // GoogleJsonWebSignature.ValidationSettings settings = new GoogleJsonWebSignature.ValidationSettings();
                // settings.Audience = new List<string>() { _authSettings.Google!.Id };

                // GoogleJsonWebSignature.Payload payload = GoogleJsonWebSignature.ValidateAsync(model.Token, settings).Result;
                // token = await CheckAuth(ProviderAuth.Google, payload.JwtId);
                throw new Exception("Not have provider.");
            }
            else if (model.Provider == ProviderAuth.Microsoft)
            {
                // MicrosoftJsonWebSignature
                throw new Exception("Not have provider.");
            }
            else if (model.Provider == ProviderAuth.Facebook)
            {
                // string tokenValidationUrl = "https://graph.facebook.com/debug_token?input_token={0}&access_token={1}|{2}";
                // var formattedUrl = string.Format(tokenValidationUrl, model.Token, _authSettings.Facebook!.Id);

                // var result = await _httpClientFactory.CreateClient().GetAsync(formattedUrl);

                // result.EnsureSuccessStatusCode();

                // var response = await result.Content.ReadAsStringAsync();
                // return JsonConvert.DeserializeObject<FacebookTokenValidationResult>(response);
                throw new Exception("Not have provider.");
            }
            else if (model.Provider == ProviderAuth.Twitter)
            {
                throw new Exception("Not have provider.");
            }
            else if (model.Provider == ProviderAuth.Line)
            {
                throw new Exception("Not have provider.");
            }
            else
            {
                throw new Exception("Not have provider.");
            }

            _unitOfWork.Commit();
            return token;
        }
        catch (Exception ex)
        {
            _unitOfWork.Rollback();
            throw new AppException(ex.Message);
        }
    }
    public Task<bool> ChangePassword(Guid userId, string oldPass, string newPass, string confPass)
    {
        try
        {
            throw new NotImplementedException();
        }
        catch (Exception ex)
        {
            throw new AppException(ex.Message);
        }
    }
    public async Task<TokenResponse> RefreshToken(RefreshTokenRequest model)
    {
        try
        {
            _unitOfWork.BeginTransaction();
            var userRepo = _unitOfWork.GetRepository<User>();
            var userId = _jwtToken.ValidateToken(model.AccessToken);
            if (userId == null) throw new AppException("Invalid access token.");

            var sessionRepo = _unitOfWork.GetRepository<AuthSession>();
            var session = sessionRepo.FirstOrDefault(x => x.UserId == userId && x.Provider == model.Provider && x.RefreshToken == model.RefreshToken);
            if (session == null) throw new AppException("Invalid refresh token.");

            var jwtToken = _jwtToken.GenerateToken((Guid)userId);
            var newRefreshToken = _jwtToken.GenerateRefreshToken();

            session.RefreshToken = newRefreshToken;
            session.UpdatedDate = DateTime.Now;
            session.UpdatedId = userId;
            sessionRepo.Update(session);
            // save changes to db
            await _unitOfWork.SaveChangesAsync();

            var result = new TokenResponse()
            {
                AccessToken = jwtToken,
                RefreshToken = newRefreshToken,
            };

            _unitOfWork.Commit();
            return result;
        }
        catch (Exception ex)
        {
            _unitOfWork.Rollback();
            throw new AppException(ex.Message);
        }

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
    private Task<TokenResponse> CheckAuth(string providerName, string payloadId)
    {
        var userRepo = _unitOfWork.GetRepository<User>();
        var authRepo = _unitOfWork.GetRepository<AuthProvider>();
        var sessionRepo = _unitOfWork.GetRepository<AuthSession>();
        var token = new TokenResponse();
        var auth = authRepo.FirstOrDefault(x => x.Provider == providerName && x.Token == payloadId);
        Guid userId;
        if (auth != null)
        {
            userId = auth.UserId;
        }
        else
        {
            var newUser = new User();
            userRepo.Add(newUser);
            _unitOfWork.SaveChanges();

            userId = newUser.Id;

            var provider = new AuthProvider();
            provider.SetProvider(userId, providerName, payloadId);
            authRepo.Add(provider);
            _unitOfWork.SaveChanges();
        }

        var accessToken = _jwtToken.GenerateToken(userId);
        var refreshToken = _jwtToken.GenerateRefreshToken();
        var session = new AuthSession();
        session.SetSession(userId, refreshToken);
        sessionRepo.Add(session);
        _unitOfWork.SaveChanges();

        token.AccessToken = accessToken;
        token.RefreshToken = refreshToken;

        return Task.FromResult(token);
    }
}