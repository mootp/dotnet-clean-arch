namespace Application.Dtos.Request;

public class UserRequest
{
    public string Code { get; set; } = null!;
    public string? Name { get; set; }
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
}