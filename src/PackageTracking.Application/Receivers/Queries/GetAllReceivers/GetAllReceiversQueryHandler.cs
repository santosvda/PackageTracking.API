using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PackageTracking.Application.Receivers.Dtos;
using PackageTracking.Domain.Repositories;

namespace PackageTracking.Application.Receivers.Queries.GetAllReceivers;
public class GetAllReceiversQueryHandler(ILogger<GetAllReceiversQueryHandler> logger
    , IMapper mapper
    , IReceiverRepository receiverRepository
    ) : IRequestHandler<GetAllReceiversQuery, IEnumerable<ReceiverDto>>
{
    public async Task<IEnumerable<ReceiverDto>> Handle(GetAllReceiversQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all receivers");
        var receivers = await receiverRepository.GetAllAsync();
        var receiversDto = mapper.Map<IEnumerable<ReceiverDto>>(receivers);

        return receiversDto;
    }
}
