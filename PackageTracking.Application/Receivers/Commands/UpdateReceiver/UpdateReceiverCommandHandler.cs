using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PackageTracking.Domain.Entities;
using PackageTracking.Domain.Excpetions;
using PackageTracking.Domain.Repositories;

namespace PackageTracking.Application.Receivers.Commands.UpdateReceiver;
public class UpdateReceiverCommandHandler(ILogger<UpdateReceiverCommandHandler> logger
    , IMapper mapper
    , IReceiverRepository receiverRepository
    ) : IRequestHandler<UpdateReceiverCommand>
{
    public async Task Handle(UpdateReceiverCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating a receiver with Id: {ReceiverId} with {@UpdateReceiver}", request.Id, request);
        var receiver = mapper.Map<Receiver>(request);

        if(await receiverRepository.GetByIdAsync(receiver.Id) is null)
            throw new NotFoundException(nameof(Receiver), request.Id.ToString());

        await receiverRepository.SaveChangesAsync(receiver);
    }
}
