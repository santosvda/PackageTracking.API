using MediatR;
using PackageTracking.Application.Packages.Dtos;

namespace PackageTracking.Application.Receivers.Queries.GetAllPackagesForReceiver;
public class GetAllPackagesForReceiverQuery : IRequest<IEnumerable<PackageDto>>
{
    public int ReceiverId { get; set; }
}
