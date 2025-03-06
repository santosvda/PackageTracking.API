using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PackageTracking.Application.Users;
using PackageTracking.Domain.Entities;
using PackageTracking.Domain.Excpetions;
using PackageTracking.Domain.Repositories;

namespace PackageTracking.Application.Packagess.Commands.CreatePackage;
public class CreatePackageCommandHandler(ILogger<CreatePackageCommandHandler> logger
    , IMapper mapper
    , IPackageRepository packageRepository
    , IReceiverRepository receiverRepository
    , IUserContext userContext
    ) : IRequestHandler<CreatePackageCommand, int>
{
    public async Task<int> Handle(CreatePackageCommand request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();

        logger.LogInformation("{UserName} {UserId} is sreating a new package {@Package}"
            , currentUser!.Email
            , currentUser.Id
            , request);

        var receiver = await receiverRepository.GetByIdAsync(request.ReceiverId)
            ?? throw new NotFoundException(nameof(Receiver), request.ReceiverId.ToString());

        var package = mapper.Map<Package>(request);
        package.OwnerId = currentUser.Id;
        var id = await packageRepository.CreateAsync(package);

        return id;
    }
}
