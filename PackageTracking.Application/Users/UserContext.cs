﻿using Microsoft.AspNetCore.Http;
using PackageTracking.Domain.Constants;
using System.Security.Claims;

namespace PackageTracking.Application.Users;

public interface IUserContext
{
    CurrentUser? GetCurrentUser();
}

public class UserContext (IHttpContextAccessor httpContextAccessor) : IUserContext
{
    public CurrentUser? GetCurrentUser()
    {
        var user = httpContextAccessor?.HttpContext?.User;

        if (user == null)
            throw new InvalidOperationException("User context is not present");

        if (user.Identity is null || !user.Identity.IsAuthenticated)
            return null;

        var userId = user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
        var email = user.FindFirst(c => c.Type == ClaimTypes.Email)!.Value;
        var roles = user.Claims.Where(c => c.Type == ClaimTypes.Email)!.Select(c => c.Value);
        var nationality = user.FindFirst(c => c.Type == AppClaimTypes.Nationality)?.Value;

        var dateofBirthString = user.FindFirst(c => c.Type == AppClaimTypes.DateofBirth)?.Value;
        var dateofBirth = dateofBirthString == null 
            ? (DateOnly?)null 
            : DateOnly.ParseExact(dateofBirthString, "yyyy-MM-dd");

        return new CurrentUser(userId, email, roles, nationality, dateofBirth);
    }
}
