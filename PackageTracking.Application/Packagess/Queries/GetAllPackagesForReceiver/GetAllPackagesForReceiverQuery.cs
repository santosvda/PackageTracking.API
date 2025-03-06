using MediatR;
using PackageTracking.Application.Common;
using PackageTracking.Application.Packages.Dtos;
using PackageTracking.Domain.Constants;

namespace PackageTracking.Application.Packagess.Queries.GetAllPackagesForReceiver;
public class GetAllPackagesForReceiverQuery : IRequest<PageResult<PackageDto>>
{
    public int ReceiverId { get; set; }
    public string? SearchDescription { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? SortBy { get; set; }
    public SortDirection SortDirection { get; set; }
}
