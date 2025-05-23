﻿namespace PackageTracking.Application.Users;

public record CurrentUser(string Id, string Email, IEnumerable<string> Roles, string? Nationality, DateOnly? DateofBirth)
{
    public bool IsInRole(string role) => Roles.Contains(role);
}
