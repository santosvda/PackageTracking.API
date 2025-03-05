using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PackageTracking.Application.Packages.Dtos;
using PackageTracking.Domain.Entities;
using PackageTracking.Domain.Excpetions;
using PackageTracking.Domain.Repositories;

namespace PackageTracking.Application.Receivers.Queries.GetAllPackagesForReceiver;
public class GetAllPackagesForReceiverQueryHandler(ILogger<GetAllPackagesForReceiverQueryHandler> logger
    , IMapper mapper
    , IPackageRepository packageRepository
    , IReceiverRepository receiverRepository
    ) : IRequestHandler<GetAllPackagesForReceiverQuery, IEnumerable<PackageDto>>
{
    public async Task<IEnumerable<PackageDto>> Handle(GetAllPackagesForReceiverQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all packages");

        var receiver = await receiverRepository.GetByIdAsync(request.ReceiverId)
            ?? throw new NotFoundException(nameof(Receiver), request.ReceiverId.ToString());

        var packages = await packageRepository.GetAllAsync();
        var packageDto = mapper.Map<IEnumerable<PackageDto>>(packages);

        return packageDto;
    }
}
