
using DotnetCoding.Core.Models;
using DotnetCoding.Infrastructure;
public static class DataSeeder
{
    public static void CreateSeedData(DbContextClass context)
    {


        var status = new List<Status>
                    {
                        new Status { Name = "Pending",Description="TBD" },
                        new Status { Name = "Rejected",Description="TBD"  },
                        new Status { Name = "Approved" ,Description="TBD" },
                    };

        context.AddRange(status);


        var updateTypes = new List<UpdateType>
                    {
                        new UpdateType { Name = "MoreThan5000Price",Description="TBD"  },
                        new UpdateType { Name = "Delete",Description="TBD"  },
                          new UpdateType { Name = "MoreThan50Percent",Description="TBD"  },
                            new UpdateType { Name = "MoreThan10000Price",Description="TBD"  },
                    };

        context.AddRange(updateTypes);
        context.SaveChanges();

    }
}