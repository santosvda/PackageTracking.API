using MediatR;

namespace PackageTracking.Application.Users.Commands;

public class UpdateUserDetailsCommand : IRequest
{
    public string? Nationality { get; set; }
    public DateOnly? DateofBirth { get; set; }
}
