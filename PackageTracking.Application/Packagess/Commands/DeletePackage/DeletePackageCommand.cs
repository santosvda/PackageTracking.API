using MediatR;

namespace PackageTracking.Application.Receivers.Commands.DeletePackage;
public class DeletePackageCommand(int receiverId, int id) : IRequest
{
    public int ReceiverId { get; } = receiverId;
    public int Id { get; } = id;
}
