using FluentAssertions;
using PackageTracking.Domain.Constants;
using Xunit;

namespace PackageTracking.Application.Users.Tests;

public class CurrentUserTests
{
    [Theory()]
    [InlineData(UserRoles.Admin)]
    [InlineData(UserRoles.User)]
    public void IsInRole_WithMatchingRole_ShouldReturnTrue(string roleName)
    {
        // arrange
        var currentUser = new CurrentUser("1", "teste.test.com", [UserRoles.Admin, UserRoles.User], null, null);

        // act
        var result = currentUser.IsInRole(roleName);

        // assert
        result.Should().BeTrue();
    }

    [Fact()]
    public void IsInRole_WithMatchingRole_ShouldReturnFalse()
    {
        // arrange
        var currentUser = new CurrentUser("1", "teste.test.com", [UserRoles.Admin, UserRoles.User], null, null);

        // act
        var result = currentUser.IsInRole(UserRoles.Owner);

        // assert
        result.Should().BeFalse();
    }

    [Fact()]
    public void IsInRole_WithNoMatchingRole_ShouldReturnFalse()
    {
        // arrange
        var currentUser = new CurrentUser("1", "teste.test.com", [UserRoles.Admin, UserRoles.User], null, null);

        // act
        var result = currentUser.IsInRole(UserRoles.Admin.ToLower());

        // assert
        result.Should().BeFalse();
    }
}