using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PackageTracking.Domain.Entities;
using PackageTracking.Domain.Excpetions;
using PackageTracking.Domain.Repositories;

namespace PackageTracking.Application.Receivers.Commands.DeleteReceiver;
public class DeleteReceiverCommandHandler(ILogger<DeleteReceiverCommandHandler> logger
    , IReceiverRepository receiverRepository
    ) : IRequestHandler<DeleteReceiverCommand>
{
    public async Task Handle(DeleteReceiverCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Inserting a new receiver {ReceiverId}", request.Id);

        var receiver = await receiverRepository.GetByIdAsync(request.Id) 
            ?? throw new NotFoundException(nameof(Receiver), request.Id.ToString());

        await receiverRepository.DeleteAsync(receiver);
    }
}
