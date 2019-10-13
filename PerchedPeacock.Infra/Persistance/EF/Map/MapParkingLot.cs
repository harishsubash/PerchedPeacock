using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PerchedPeacock.Domain;

namespace PerchedPeacock.Infra.Persistance.EF.Map
{
    public class MapParkingLot : IEntityTypeConfiguration<ParkingLot>
    {
        public void Configure(EntityTypeBuilder<ParkingLot> builder)
        {
            builder.HasKey(x => x.ParkingLotId);
        }
    }
}
