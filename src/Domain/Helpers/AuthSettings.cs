namespace Domain.Helpers;

public class AuthSettings
{
    public const string SectionName = "AuthSettings";

    public string Google { get; init; } = null!;
    public string Microsoft { get; init; } = null!;
}
