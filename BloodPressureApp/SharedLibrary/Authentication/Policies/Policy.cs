namespace SharedLibrary.Authentication.Policies;

public sealed class Policy
{
    internal const string Admin = "Admin";
    internal const string User = "User";
    internal const int AdminId = 1;
    internal const int UserId = 2;

    public int Id { get; init; }
    public required string Name { get; init; }
}