using PerchedPeacock.Core;
using System;

namespace PerchedPeacock.Domain
{
    public class ParkingSlotId : Value<ParkingSlotId>
    {
        public ParkingSlotId(Guid id) => Id = id;

        public Guid Id { get; set; }

        protected ParkingSlotId() { }
    }
}
