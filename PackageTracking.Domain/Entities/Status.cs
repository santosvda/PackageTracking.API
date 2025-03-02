namespace PackageTracking.Domain.Entities
{
    public class Status
    {
        public int Id { get; set; }
        public int PackageId { get; set; }
        public string Description { get; set; } = default!;
        public DateTime DateTime { get; set; }
    }
}
