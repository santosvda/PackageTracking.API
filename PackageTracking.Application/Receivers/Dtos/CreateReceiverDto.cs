namespace PackageTracking.Application.Receivers.Dtos;

public record CreateReceiverDto
{
    public string Description { get; init; } = default!;
    public string? ContactEmail { get; init; }
    public string? ContactNumber { get; init; }
    public string? DocumentNumber { get; init; }
}

