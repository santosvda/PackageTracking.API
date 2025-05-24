using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PackageTracking.Application.Common;
using PackageTracking.Application.Packages.Dtos;
using PackageTracking.Domain.Entities;
using PackageTracking.Domain.Excpetions;
using PackageTracking.Domain.Repositories;

namespace PackageTracking.Application.Packagess.Queries.GetAllPackagesForReceiver;
public class GetAllPackagesForReceiverQueryHandler(ILogger<GetAllPackagesForReceiverQueryHandler> logger
    , IMapper mapper
    , IPackageRepository packageRepository
    , IReceiverRepository receiverRepository
    ) : IRequestHandler<GetAllPackagesForReceiverQuery, PageResult<PackageDto>>
{
    public async Task<PageResult<PackageDto>> Handle(GetAllPackagesForReceiverQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all packages");

        var receiver = await receiverRepository.GetByIdAsync(request.ReceiverId)
            ?? throw new NotFoundException(nameof(Receiver), request.ReceiverId.ToString());

        var (packages, totalCount) = await packageRepository.GetAllMatchingAsync(request.SearchDescription
            , request.PageNumber
            , request.PageSize
            , request.SortBy
            , request.SortDirection
            );

        var packageDto = mapper.Map<IEnumerable<PackageDto>>(packages);

        var result = new PageResult<PackageDto>(packageDto, totalCount, request.PageSize, request.PageNumber);

        return result;
    }
}
