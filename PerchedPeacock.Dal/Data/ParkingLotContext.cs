using Microsoft.EntityFrameworkCore;
using PerchedPeacock.Domain.Model;

namespace PerchedPeacock.Dal.Data
{
    public class ParkingLotContext : DbContext
    {
        public ParkingLotContext (DbContextOptions<ParkingLotContext> options)
            : base(options)
        {
        }

        public DbSet<ParkingLot> ParkingLot { get; set; }
        public DbSet<ParkingSlot> ParkingSlot { get; set; }

    }
}
