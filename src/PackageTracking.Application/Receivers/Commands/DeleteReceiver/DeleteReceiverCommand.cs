using MediatR;

namespace PackageTracking.Application.Receivers.Commands.DeleteReceiver;
public class DeleteReceiverCommand(int id) : IRequest
{
    public int Id { get; } = id;
}
