using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PackageTracking.Application.Receivers.Dtos;
using PackageTracking.Domain.Entities;
using PackageTracking.Domain.Excpetions;
using PackageTracking.Domain.Repositories;

namespace PackageTracking.Application.Receivers.Queries.GetReceiverById;
public class GetReceiverByIdQueryHandler(ILogger<GetReceiverByIdQueryHandler> logger
    , IMapper mapper
    , IReceiverRepository receiverRepository
    ) : IRequestHandler<GetReceiverByIdQuery, ReceiverDto>
{
    public async Task<ReceiverDto> Handle(GetReceiverByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting receiver by Id: {ReceiverId}", request.Id);

        var receiver = await receiverRepository.GetByIdAsync(request.Id)
            ?? throw new NotFoundException(nameof(Receiver), request.Id.ToString());

        var receiverDto = mapper.Map<ReceiverDto>(receiver);
        return receiverDto;
    }
}
