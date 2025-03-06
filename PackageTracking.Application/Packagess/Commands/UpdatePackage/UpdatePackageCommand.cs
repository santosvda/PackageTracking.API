using MediatR;

namespace PackageTracking.Application.Packagess.Commands.UpdatePackage;
public class UpdatePackageCommand() : IRequest
{
    public int Id { get; set; }
    public int ReceiverId { get; set; }
    public string Description { get; set; } = default!;
    public decimal Weight { get; set; }
    public bool Delivered { get; set; }
    public string? City { get; set; }
    public string? Street { get; set; }
    public string? PostalCode { get; set; }
    public string? Country { get; set; }
}
