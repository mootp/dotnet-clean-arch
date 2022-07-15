namespace Application.Dtos.Response;

public class UserResponse
{
    public Guid Id { get; set; }
    public string Code { get; set; } = null!;
    public string? Name { get; set; }
    public string Username { get; set; } = null!;
}