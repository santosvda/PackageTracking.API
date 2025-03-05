namespace PackageTracking.Application.Statuses.Dtos;
public record StatusDto
{
    public string Description { get; set; } = default!;
    public DateTime DateTime { get; set; }
}
