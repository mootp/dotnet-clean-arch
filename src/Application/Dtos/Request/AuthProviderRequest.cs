namespace Application.Dtos.Request;

public class AuthProviderRequest
{
    public string Provider { get; set; } = null!;
    public string Token { get; set; } = null!;
}