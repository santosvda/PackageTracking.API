using PackageTracking.Domain.Entities;
using PackageTracking.Infrastructure.Persistence;
using PackageTracking.Infrastructure.Seeders.Interfaces;

namespace PackageTracking.Infrastructure.Seeders;
internal class ReceiverSeeder (PackageTrackingDbContext dbContext) : IReceiverSeeder
{
    public async Task Seed() 
    {
        if (await dbContext.Database.CanConnectAsync())
            if (!dbContext.Receivers.Any())
            {
                var Receiver = GetReceiver();
                await dbContext.Receivers.AddRangeAsync(Receiver);
                await dbContext.SaveChangesAsync();
            }

    }
    private IEnumerable<Receiver> GetReceiver()
    {
        List<Receiver> Receiver = [
            new Receiver
            {
                Id = 1,
                Description = "John Doe",
                ContactEmail = "jonh@jonh.com",
                ContactNumber = "123456789",
                Packages = new List<Package>
                {
                    new Package
                    {
                        Id = 1,
                        Description = "Package 1",
                        Weight = 1.5m,
                        Delivered = false,
                        Adress = new Adress
                        {
                            Street = "Street 1",
                            City = "City 1",
                            PostalCode = "12345",
                            Country = "Country 1"
                        },
                        Status = new List<Status>
                        {
                            new Status
                            {
                                Id = 1,
                                Description = "Status 1",
                                DateTime = DateTime.Now
                            }
                        }
                    }
                }
            },
            new Receiver
            {
                Id = 2,
                Description = "Jane Doe",
                ContactEmail = "jane@jane.com",
                ContactNumber = "987654321",
                Packages = new List<Package>
                {
                    new Package
                    {
                        Id = 2,
                        Description = "Package 2",
                        Weight = 2.5m,
                        Delivered = false,
                        Adress = new Adress
                        {
                            Street = "Street 2",
                            City = "City 2",
                            PostalCode = "54321",
                            Country = "Country 2"
                        },
                        Status = new List<Status>
                        {
                            new Status
                            {
                                Id = 2,
                                Description = "Status 2",
                                DateTime = DateTime.Now
                            }
                        }
                    }
                }
            }
        ];
        return Receiver;
    }
}
