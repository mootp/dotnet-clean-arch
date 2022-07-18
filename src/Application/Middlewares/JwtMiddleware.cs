using Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Application.Middlewares;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;

    public JwtMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, IUserService userSrv, IJwtTokenGenerator jwtUtils)
    {
        var auth = context.Request.Headers["Authorization"].FirstOrDefault();
        if (string.IsNullOrEmpty(auth))
        {
            var token = auth!.Split(" ").Last();
            var userId = jwtUtils.ValidateToken(token!);
            if (userId != null)
            {
                var user = userSrv.GetUser(userId.Value);
                if (user != null)
                {
                    context.Items["User"] = user;
                }
            }
        }

        await _next(context);
    }
}