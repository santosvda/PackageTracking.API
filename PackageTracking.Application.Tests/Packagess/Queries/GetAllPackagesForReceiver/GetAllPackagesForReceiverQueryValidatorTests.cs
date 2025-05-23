using FluentValidation.TestHelper;
using PackageTracking.Application.Receivers.Commands.CreateReceiver;
using Xunit;

namespace PackageTracking.Application.Packagess.Queries.GetAllPackagesForReceiver.Tests;

public class GetAllPackagesForReceiverQueryValidatorTests
{
    //10, 15, 20, 25, 30, 50
    [Theory()]
    [InlineData(10)]
    [InlineData(15)]
    [InlineData(20)]
    [InlineData(25)]
    [InlineData(30)]
    [InlineData(50)]
    public void Validator_ForValidPageSize_ShouldNotHaveValidationErrorsForPageSize(int pageSize)
    {
        // arrange
        var command = new GetAllPackagesForReceiverQuery
        {
            PageSize = pageSize,
            PageNumber = 1
        };

        var validator = new GetAllPackagesForReceiverQueryValidator();

        // act
        var result = validator.TestValidate(command);

        // assert
        result.ShouldNotHaveValidationErrorFor(c => c.PageSize);
    }
}