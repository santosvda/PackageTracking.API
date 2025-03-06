using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PackageTracking.Domain.Entities;
using PackageTracking.Domain.Excpetions;
using PackageTracking.Domain.Interfaces;
using PackageTracking.Domain.Repositories;
using PackageTracking.Infrastructure.Authorization;

namespace PackageTracking.Application.Packagess.Commands.UpdatePackage;
public class UpdatePackageCommandHandler(ILogger<UpdatePackageCommandHandler> logger
    , IMapper mapper
    , IReceiverRepository receiverRepository
    , IPackageRepository packageRepository
    , IPackagesAuthorizationService packagesAuthorizationService
    ) : IRequestHandler<UpdatePackageCommand>
{
    public async Task Handle(UpdatePackageCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating a receiver with Id: {ReceiverId} with {@UpdatePackage}", request.Id, request);

        var receiver = await receiverRepository.GetByIdAsync(request.ReceiverId)
            ?? throw new NotFoundException(nameof(Receiver), request.ReceiverId.ToString());

        var package = await packageRepository.GetByIdAsync(request.ReceiverId, request.Id)
            ?? throw new NotFoundException(nameof(Package), request.Id.ToString());

        mapper.Map(request, package);

        if (!packagesAuthorizationService.Authorize(package, ResourceOperation.Update))
            throw new ForbiddenAccessException();

        await packageRepository.SaveChanges();
    }
}
