using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PackageTracking.Application.Packages.Dtos;
using PackageTracking.Domain.Entities;
using PackageTracking.Domain.Excpetions;
using PackageTracking.Domain.Repositories;

namespace PackageTracking.Application.Packagess.Queries.GetAllPackagesForReceiverById;
public class GetAllPackagesForReceiverByIdQueryHandler(ILogger<GetAllPackagesForReceiverByIdQueryHandler> logger
    , IMapper mapper
    , IPackageRepository packageRepository
    , IReceiverRepository receiverRepository
    ) : IRequestHandler<GetAllPackagesForReceiverByIdQuery, PackageDto>
{
    public async Task<PackageDto> Handle(GetAllPackagesForReceiverByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting package by Id: {PackageId}", request.Id );

        var receiver = await receiverRepository.GetByIdAsync(request.ReceiverId)
            ?? throw new NotFoundException(nameof(Receiver), request.ReceiverId.ToString());

        var package = await packageRepository.GetByIdAsync(request.ReceiverId, request.Id)
            ?? throw new NotFoundException(nameof(Package), request.Id.ToString());

        var packageDto = mapper.Map<PackageDto>(package);
        return packageDto;
    }
}
