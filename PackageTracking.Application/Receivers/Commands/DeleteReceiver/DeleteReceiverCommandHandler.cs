using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PackageTracking.Domain.Entities;
using PackageTracking.Domain.Repositories;

namespace PackageTracking.Application.Receivers.Commands.DeleteReceiver;
public class DeleteReceiverCommandHandler(ILogger<DeleteReceiverCommandHandler> logger
    , IReceiverRepository receiverRepository
    ) : IRequestHandler<DeleteReceiverCommand, bool>
{
    public async Task<bool> Handle(DeleteReceiverCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Inserting a new receiver");

        var receiver = await receiverRepository.GetByIdAsync(request.Id);

        if (receiver is null)
            return false;

        await receiverRepository.DeleteAsync(receiver);

        return true;
    }
}
