using Microsoft.EntityFrameworkCore;
using PerchedPeacock.Domain;
using PerchedPeacock.Infra.Persistance.EF.Configuration;

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
            modelBuilder.ApplyConfiguration(new ParkingLotEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ParkingSlotEntityTypeConfiguration());
        }

        public DbSet<ParkingLot> ParkingLot { get; set; }

    }
}
