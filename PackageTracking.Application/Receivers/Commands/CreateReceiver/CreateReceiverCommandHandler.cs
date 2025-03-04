using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PackageTracking.Domain.Entities;
using PackageTracking.Domain.Repositories;

namespace PackageTracking.Application.Receivers.Commands.CreateReceiver;
public class CreateReceiverCommandHandler(ILogger<CreateReceiverCommandHandler> logger
    , IMapper mapper
    , IReceiverRepository receiverRepository
    ) : IRequestHandler<CreateReceiverCommand, int>
{
    public async Task<int> Handle(CreateReceiverCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Inserting a new receiver {@Receiver}", request);
        var receiver = mapper.Map<Receiver>(request);

        var id = await receiverRepository.CreateAsync(receiver);

        return id;
    }
}
