using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PackageTracking.Domain.Entities;
using PackageTracking.Domain.Excpetions;
using PackageTracking.Domain.Repositories;

namespace PackageTracking.Application.Receivers.Commands.DeletePackage;
public class DeletePackageCommandHandler(ILogger<DeletePackageCommandHandler> logger
    , IReceiverRepository receiverRepository
    , IPackageRepository packageRepository
    ) : IRequestHandler<DeletePackageCommand>
{
    public async Task Handle(DeletePackageCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting a new package {PackageId}", request.Id);

        var receiver = await receiverRepository.GetByIdAsync(request.ReceiverId)
            ?? throw new NotFoundException(nameof(Receiver), request.ReceiverId.ToString());

        var package = await packageRepository.GetByIdAsync(request.ReceiverId, request.Id) 
            ?? throw new NotFoundException(nameof(Package), request.Id.ToString());

        await packageRepository.DeleteAsync(package);
    }
}
