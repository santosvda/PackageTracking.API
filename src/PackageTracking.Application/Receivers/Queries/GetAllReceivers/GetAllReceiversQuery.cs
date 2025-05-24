using MediatR;
using PackageTracking.Application.Receivers.Dtos;

namespace PackageTracking.Application.Receivers.Queries.GetAllReceivers;
public class GetAllReceiversQuery : IRequest<IEnumerable<ReceiverDto>>;
