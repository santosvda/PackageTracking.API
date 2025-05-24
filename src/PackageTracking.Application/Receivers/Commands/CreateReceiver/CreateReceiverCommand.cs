using MediatR;

namespace PackageTracking.Application.Receivers.Commands.CreateReceiver;
public class CreateReceiverCommand : IRequest<int>
{
    public string Description { get; init; } = default!;
    public string? ContactEmail { get; init; }
    public string? ContactNumber { get; init; }
    public string? DocumentNumber { get; init; }
}
