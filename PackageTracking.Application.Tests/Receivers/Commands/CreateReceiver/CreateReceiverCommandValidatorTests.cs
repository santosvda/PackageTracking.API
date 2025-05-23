using FluentValidation.TestHelper;
using Xunit;

namespace PackageTracking.Application.Receivers.Commands.CreateReceiver.Tests;

public class CreateReceiverCommandValidatorTests
{
    [Fact()]
    public void Validator_ForValidCommand_ShouldNotHaveValidationErrors()
    {
        // arrange
        var command = new CreateReceiverCommand
        {
            Description = "Valid Description",
            DocumentNumber = "123456789",
            ContactEmail = "teste@test.com",
            ContactNumber = "12-123456789"
        };

        var validator = new CreateReceiverCommandValidator();

        // act
        var result = validator.TestValidate(command);

        // assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact()]
    public void Validator_ForInvalidCommand_ShouldHaveValidationErrors()
    {
        // arrange
        var command = new CreateReceiverCommand
        {
            Description = "ab",
            DocumentNumber = "12345678",
            ContactEmail = "invalid-email",
            ContactNumber = "invalid-phone"
        };
        var validator = new CreateReceiverCommandValidator();

        // act
        var result = validator.TestValidate(command);

        // assert
        result.ShouldHaveValidationErrorFor(x => x.Description);
        result.ShouldHaveValidationErrorFor(x => x.DocumentNumber);
        result.ShouldHaveValidationErrorFor(x => x.ContactEmail);
        result.ShouldHaveValidationErrorFor(x => x.ContactNumber);
    }
}