using MediatR;

namespace PackageTracking.Application.Users.Commands.UpdateUserDetails;

public class UpdateUserDetailsCommand : IRequest
{
    public string? Nationality { get; set; }
    public DateOnly? DateofBirth { get; set; }
}
