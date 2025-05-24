using MediatR;

namespace PackageTracking.Application.Packagess.Commands.DeletePackage;
public class DeletePackageCommand(int receiverId, int id) : IRequest
{
    public int ReceiverId { get; } = receiverId;
    public int Id { get; } = id;
}
