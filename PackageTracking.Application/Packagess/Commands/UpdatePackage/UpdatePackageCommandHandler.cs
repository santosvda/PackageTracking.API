using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PackageTracking.Domain.Entities;
using PackageTracking.Domain.Excpetions;
using PackageTracking.Domain.Repositories;

namespace PackageTracking.Application.Receivers.Commands.UpdatePackage;
public class UpdatePackageCommandHandler(ILogger<UpdatePackageCommandHandler> logger
    , IMapper mapper
    , IReceiverRepository receiverRepository
    , IPackageRepository packageRepository
    ) : IRequestHandler<UpdatePackageCommand>
{
    public async Task Handle(UpdatePackageCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating a receiver with Id: {ReceiverId} with {@UpdatePackage}", request.Id, request);
        var package = mapper.Map<Package>(request);

        var receiver = await receiverRepository.GetByIdAsync(request.ReceiverId)
            ?? throw new NotFoundException(nameof(Receiver), request.ReceiverId.ToString());

        var aux = await packageRepository.GetByIdAsync(request.ReceiverId, request.Id)
            ?? throw new NotFoundException(nameof(Package), request.Id.ToString());

        await packageRepository.SaveChangesAsync(package);
    }
}
