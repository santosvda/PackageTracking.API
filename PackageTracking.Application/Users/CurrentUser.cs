namespace PackageTracking.Application.Users;

public record CurrentUser(string Id, string Email, IEnumerable<string> roles, string? Nationality, DateOnly? DateofBirth)
{
    public bool IsInRole(string role) => roles.Contains(role);
}
