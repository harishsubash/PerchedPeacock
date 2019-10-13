using PerchedPeacock.Core;
using System;

namespace PerchedPeacock.Domain
{
    public class ParkingLotId : Value<ParkingLotId>
    {
        public ParkingLotId(Guid value) => Value = value;

        public Guid Value { get; }

        protected ParkingLotId() { }
    }
}
