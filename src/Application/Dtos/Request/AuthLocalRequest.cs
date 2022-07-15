namespace Application.Dtos.Request;

public class AuthLocalRequest
{
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
}