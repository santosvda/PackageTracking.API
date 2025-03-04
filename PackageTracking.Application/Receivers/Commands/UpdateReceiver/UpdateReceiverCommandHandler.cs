using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PackageTracking.Domain.Entities;
using PackageTracking.Domain.Repositories;

namespace PackageTracking.Application.Receivers.Commands.UpdateReceiver;
public class UpdateReceiverCommandHandler(ILogger<UpdateReceiverCommandHandler> logger
    , IMapper mapper
    , IReceiverRepository receiverRepository
    ) : IRequestHandler<UpdateReceiverCommand, bool>
{
    public async Task<bool> Handle(UpdateReceiverCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating a receiver");
        var receiver = mapper.Map<Receiver>(request);

        if(await receiverRepository.GetByIdAsync(receiver.Id) is null)
            return false;

        await receiverRepository.SaveChangesAsync(receiver);

        return true;
    }
}
