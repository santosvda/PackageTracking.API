namespace PackageTracking.Domain.Entities
{
    public class Package
    {
        public int Id { get; set; }
        public string Description { get; set; } = default!;
        public decimal Weight { get; set; }
        public bool Delivered { get; set; }

        public List<Status> Status { get; set; } = new();
    }
}
