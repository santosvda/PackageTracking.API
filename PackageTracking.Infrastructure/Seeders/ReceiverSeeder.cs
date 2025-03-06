using Microsoft.AspNetCore.Identity;
using PackageTracking.Domain.Constants;
using PackageTracking.Domain.Entities;
using PackageTracking.Infrastructure.Persistence;
using PackageTracking.Infrastructure.Seeders.Interfaces;

namespace PackageTracking.Infrastructure.Seeders;
internal class ReceiverSeeder (PackageTrackingDbContext dbContext) : IReceiverSeeder
{
    public async Task Seed() 
    {
        //if (await dbContext.Database.CanConnectAsync())
        //    if (!dbContext.Receivers.Any())
        //    {
        //        var Receiver = GetReceiver();
        //        await dbContext.Receivers.AddRangeAsync(Receiver);
        //        await dbContext.SaveChangesAsync();
        //    }

        if (!dbContext.Roles.Any())
        {
            var roles = GetRoles();
            await dbContext.Roles.AddRangeAsync(roles);
            await dbContext.SaveChangesAsync();
        }

    }

    private IEnumerable<IdentityRole> GetRoles()
    {
        List<IdentityRole> roles =
        [
            new (UserRoles.Admin){
                NormalizedName = UserRoles.Admin.ToUpper()
            },
            new (UserRoles.Owner){
                NormalizedName = UserRoles.Owner.ToUpper()
            },
            new (UserRoles.User) {
                NormalizedName = UserRoles.User.ToUpper() 
            },
        ];
        return roles;
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
                DocumentNumber = "123456789",
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
                        Statuses = new List<Status>
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
                DocumentNumber = "987654321",
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
                        Statuses = new List<Status>
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
