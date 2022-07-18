namespace Domain.Helpers;

public class AuthSettings
{
    public const string SectionName = "AuthSettings";

    public AuthData? Google { get; init; }
    public AuthData? Microsoft { get; init; }
    public AuthData? Facebook { get; init; }
    public AuthData? Twitter { get; init; }
}

public class AuthData
{
    public string Id { get; init; } = null!;
    public string Secret { get; init; } = null!;
}
