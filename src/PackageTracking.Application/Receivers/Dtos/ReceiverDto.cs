using PackageTracking.Application.Packages.Dtos;

namespace PackageTracking.Application.Receivers.Dtos;
public record ReceiverDto
{
    public int Id { get; init; }
    public string Description { get; init; } = default!;
    public string? ContactEmail { get; init; }
    public string? ContactNumber { get; init; }
    public IEnumerable<PackageDto>? Packages { get; init; }
}

