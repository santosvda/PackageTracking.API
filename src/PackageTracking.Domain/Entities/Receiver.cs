namespace PackageTracking.Domain.Entities
{
    public class Receiver
    {
        public int Id { get; set; }
        public string Description { get; set; } = default!;
        public string? ContactEmail { get; set; }
        public string? ContactNumber { get; set; }
        public string DocumentNumber { get; set; } = default!;

        public List<Package> Packages { get; set; } = new();
    }
}
