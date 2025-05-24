using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using PackageTracking.Domain.Constants;
using System.Security.Claims;
using Xunit;

namespace PackageTracking.Application.Users.Tests;

public class UserContextTests
{
    [Fact()]
    public void GetCurrentUserTest_WithAuthenticatedUser_ShouldReturnCurrentUser()
    {
        // arrange
        var dateofBirth = DateOnly.Parse("1999-01-01");
        var httpContextAccessorMock = new Mock<IHttpContextAccessor>();

        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, "1"),
            new Claim(ClaimTypes.Email, "test@test.com"),
            new Claim(ClaimTypes.Role, UserRoles.Admin),
            new Claim(ClaimTypes.Role, UserRoles.User),
            new Claim("Nationality", "Brazilian"),
            new Claim("DateofBirth", "1999-01-01"),

        };
        var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "Test"));

        httpContextAccessorMock.Setup(x => x.HttpContext).Returns(new DefaultHttpContext() { User = user });

        var userContext = new UserContext(httpContextAccessorMock.Object);

        // act
        var currentUser = userContext.GetCurrentUser();

        // assert
        currentUser.Should().NotBeNull();
        currentUser.Id.Should().Be("1");
        currentUser.Email.Should().Be("test@test.com");
        currentUser.Roles.Should().Contain(UserRoles.Admin);
        currentUser.Roles.Should().Contain(UserRoles.User);
        currentUser.Nationality.Should().Be("Brazilian");
        currentUser.DateofBirth.Should().Be(dateofBirth);
    }

    [Fact()]
    public void GetCurrentUserTest_WithUserContextNotPresent_ThrowInvalidOperationException()
    {
        // arrange
        var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
        httpContextAccessorMock.Setup(x => x.HttpContext).Returns((HttpContext?)null);

        var userContext = new UserContext(httpContextAccessorMock.Object);
        // act
        Action act = () => userContext.GetCurrentUser();

        // assert
        act.Should().Throw<InvalidOperationException>()
            .WithMessage("User context is not present");
    }
}