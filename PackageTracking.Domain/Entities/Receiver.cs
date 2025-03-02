namespace PackageTracking.Domain.Entities
{
    public class Receiver
    {
        public int Id { get; set; }
        public string Description { get; set; } = default!;
        public bool HasDelivery { get; set; }

        public string? ContactEmail { get; set; }
        public string? ContactNumber { get; set; }

        public Adress? Adress { get; set; }
        public List<Package> Packages { get; set; } = new();

    }
}
