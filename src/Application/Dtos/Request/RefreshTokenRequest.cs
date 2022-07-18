namespace Application.Dtos.Request;

public class RefreshTokenRequest
{
    public string Provider { get; set; } = null!;
    public string AccessToken { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
}