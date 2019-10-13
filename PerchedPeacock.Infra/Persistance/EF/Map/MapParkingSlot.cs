using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PerchedPeacock.Domain;

namespace PerchedPeacock.Infra.Persistance.EF.Map
{
    public class MapParkingSlot : IEntityTypeConfiguration<ParkingSlot>
    {
        public void Configure(EntityTypeBuilder<ParkingSlot> builder)
        {
            builder.HasKey(x => x.ParkingSlotId);
        }
    }
}
