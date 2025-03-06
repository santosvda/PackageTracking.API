using FluentValidation;

namespace PackageTracking.Application.Packagess.Commands.UpdatePackage;
public class UpdatePackageCommandValidator : AbstractValidator<UpdatePackageCommand>
{
    public UpdatePackageCommandValidator()
    {
        RuleFor(x => x.Description).Length(3, 100);
        RuleFor(x => x.Weight).GreaterThanOrEqualTo(0)
            .WithMessage("Weight must be greater or equal to 0");
        RuleFor(x => x.PostalCode).Matches(@"^\d{3}-\d{4}$")
            .WithMessage("Please provide a valid postal code number XXX-XXXX");
    }
}
