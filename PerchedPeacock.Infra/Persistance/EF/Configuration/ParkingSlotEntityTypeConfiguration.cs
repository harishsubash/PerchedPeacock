using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PerchedPeacock.Domain;

namespace PerchedPeacock.Infra.Persistance.EF.Configuration
{
    public class ParkingSlotEntityTypeConfiguration : IEntityTypeConfiguration<ParkingSlot>
    {
        public void Configure(EntityTypeBuilder<ParkingSlot> builder)
        {
            builder.HasKey(x => x.ParkingSlotId);
        }
    }
}
