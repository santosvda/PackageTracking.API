using FluentValidation;
using PackageTracking.Application.Receivers.Dtos;

namespace PackageTracking.Application.Receivers.Validators;
public class CreateReceiverDtoValidator : AbstractValidator<CreateReceiverDto>
{
    public CreateReceiverDtoValidator()
    {
        RuleFor(x => x.Description).Length(3,100);

        RuleFor(x => x.ContactEmail).EmailAddress()
            .WithMessage("Please provide a valid email address");

        RuleFor(x => x.ContactNumber).Matches(@"^\d{2}-\d{9}$")
            .WithMessage("Please provide a valid phone number XX-XXXXXXXXX");

        RuleFor(x => x.DocumentNumber).Matches(@"^[0-9]{9}$")
            .WithMessage("Please provide a valid document number XXXXXXXXX");
    }
}
