using Microsoft.AspNetCore.Identity;

namespace PackageTracking.Domain.Entities;
public class User : IdentityUser
{
    public string? Nationality { get; set; }
    public DateOnly? DateofBirth { get; set; }
}
