using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using PackageTracking.Application.Users;
using PackageTracking.Domain.Entities;
using PackageTracking.Domain.Repositories;
using Xunit;

namespace PackageTracking.Application.Packagess.Commands.CreatePackage.Tests;

public class CreatePackageCommandHandlerTests
{
    [Fact()]
    public async Task Handle_ForValidCommand_ReturnsCreatedPackageId()
    {
        // arrange

        var logger = new Mock<ILogger<CreatePackageCommandHandler>>();
        var mapper = new Mock<IMapper>();
        var package = new Package();
        mapper.Setup(x => x.Map<Package>(It.IsAny<CreatePackageCommand>()))
            .Returns(package);

        var receiverRepository = new Mock<IReceiverRepository>();
        receiverRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((int id) => new Receiver
            {
                Id = id,
                Description = "Test receiver",
                DocumentNumber = "123456789",
                ContactEmail = "test@test.com",
                ContactNumber = "1",
                Packages = new List<Package>()
            });

        var packageRepository = new Mock<IPackageRepository>();
        packageRepository.Setup(x => x.CreateAsync(It.IsAny<Package>()))
            .ReturnsAsync(1);

        var userContext = new Mock<IUserContext>();
        var currentUser = new CurrentUser("1", "test@test.com", [], null , null);
        userContext.Setup(x => x.GetCurrentUser()).Returns(currentUser);


        var commandHandler = new CreatePackageCommandHandler(
            logger.Object,
            mapper.Object,
            packageRepository.Object,
            receiverRepository.Object,
            userContext.Object
        );

        var command = new CreatePackageCommand
        {
            ReceiverId = 1,
            Weight = 2.5m,
            Description = "Test package"
        };

        // act
        var result = await commandHandler.Handle(command, CancellationToken.None);

        // assert
        result.Should().Be(1);
        package.OwnerId.Should().Be(currentUser.Id);
        packageRepository.Verify(x => x.CreateAsync(It.IsAny<Package>()), Times.Once);
    }
}