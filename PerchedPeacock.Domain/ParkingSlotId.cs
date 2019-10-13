using PerchedPeacock.Core;
using System;

namespace PerchedPeacock.Domain
{
    public class ParkingSlotId : Value<ParkingSlotId>
    {
        public ParkingSlotId(Guid value) => Value = value;

        public Guid Value { get; set; }

        protected ParkingSlotId() { }
    }
}
