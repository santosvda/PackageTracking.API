using MediatR;
using PackageTracking.Application.Packages.Dtos;

namespace PackageTracking.Application.Receivers.Queries.GetAllPackagesForReceiverById;
public class GetAllPackagesForReceiverByIdQuery(int receiverId, int id) : IRequest<PackageDto>
{
    public int ReceiverId { get; } = receiverId;
    public int Id { get; } = id;
}
