using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PackageTracking.Domain.Entities;
using PackageTracking.Domain.Excpetions;
using PackageTracking.Domain.Repositories;

namespace PackageTracking.Application.Packagess.Commands.CreatePackage;
public class CreatePackageCommandHandler(ILogger<CreatePackageCommandHandler> logger
    , IMapper mapper
    , IPackageRepository packageRepository
    , IReceiverRepository receiverRepository
    ) : IRequestHandler<CreatePackageCommand, int>
{
    public async Task<int> Handle(CreatePackageCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating a new package {@Package}", request);

        var receiver = await receiverRepository.GetByIdAsync(request.ReceiverId)
            ?? throw new NotFoundException(nameof(Receiver), request.ReceiverId.ToString());

        var package = mapper.Map<Package>(request);
        var id = await packageRepository.CreateAsync(package);

        return id;
    }
}
