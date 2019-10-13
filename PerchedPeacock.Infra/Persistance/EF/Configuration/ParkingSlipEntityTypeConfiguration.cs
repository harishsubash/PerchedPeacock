using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PerchedPeacock.Domain;

namespace PerchedPeacock.Infra.Persistance.EF.Configuration
{
    public class ParkingSlipEntityTypeConfiguration : IEntityTypeConfiguration<ParkingSlip>
    {
        public void Configure(EntityTypeBuilder<ParkingSlip> builder)
        {
            builder.HasKey(x => x.ParkingSlipId);
            builder.OwnsOne(x => x.Id);
            builder.OwnsOne(x => x.Vehicle);
        }
    }
}
