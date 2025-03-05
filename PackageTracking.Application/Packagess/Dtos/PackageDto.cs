using PackageTracking.Application.Statuses.Dtos;
using PackageTracking.Domain.Entities;

namespace PackageTracking.Application.Packages.Dtos;
public record PackageDto
{
    public string Description { get; set; } = default!;
    public decimal Weight { get; set; }
    public bool Delivered { get; set; }
    public string? City { get; set; }
    public string? Street { get; set; }
    public string? PostalCode { get; set; }
    public string? Country { get; set; }
    public IEnumerable<StatusDto> Statuses { get; set; }
}
