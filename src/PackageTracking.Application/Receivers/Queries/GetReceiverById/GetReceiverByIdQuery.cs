using MediatR;
using PackageTracking.Application.Receivers.Dtos;

namespace PackageTracking.Application.Receivers.Queries.GetReceiverById;
public record GetReceiverByIdQuery : IRequest<ReceiverDto>
{
    public int Id { get; init; }
}
