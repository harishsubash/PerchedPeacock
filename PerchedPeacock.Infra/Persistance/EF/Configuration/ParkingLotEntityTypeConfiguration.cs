using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PerchedPeacock.Domain;

namespace PerchedPeacock.Infra.Persistance.EF.Configuration
{
    public class ParkingLotEntityTypeConfiguration : IEntityTypeConfiguration<ParkingLot>
    {
        public void Configure(EntityTypeBuilder<ParkingLot> builder)
        {
            builder.HasKey(x => x.ParkingLotId);
        }
    }
}
