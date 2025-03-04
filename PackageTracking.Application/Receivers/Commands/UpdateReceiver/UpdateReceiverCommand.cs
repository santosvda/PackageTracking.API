using MediatR;

namespace PackageTracking.Application.Receivers.Commands.UpdateReceiver;
public class UpdateReceiverCommand : IRequest
{
    public int Id { get; set; }
    public string Description { get; init; } = default!;
    public string? ContactEmail { get; init; }
    public string? ContactNumber { get; init; }
    public string? DocumentNumber { get; init; }
}
