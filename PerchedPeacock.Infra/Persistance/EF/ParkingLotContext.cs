using Microsoft.EntityFrameworkCore;
using PerchedPeacock.Domain;
using PerchedPeacock.Infra.Persistance.EF.Map;

namespace PerchedPeacock.Infra.Persistanace.EF
{
    public class ParkingLotContext : DbContext
    {
        public ParkingLotContext (DbContextOptions<ParkingLotContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MapParkingLot());
            modelBuilder.ApplyConfiguration(new MapParkingSlot());
        }

        public DbSet<ParkingLot> ParkingLot { get; set; }

    }
}
